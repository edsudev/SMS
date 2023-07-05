using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace EDSU_SYSTEM.Controllers
{
   // [Authorize]
    public class WalletsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public WalletsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            
        }
        [HttpGet]
        public IActionResult GetPaymentKey()
        {
            var paymentKey = Environment.GetEnvironmentVariable("PAYSTACK_TEST_KEY");
            return Json(paymentKey);
        }

        // GET: wallets
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StudentsId;
            var student = (from s in _context.Students where s.Id == user select s.StudentId).FirstOrDefault();
            var applicationDbContext = _context.UgMainWallets.Where(x => x.WalletId == student).FirstOrDefaultAsync();
            return View(await applicationDbContext);
        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> History(string id)
        {
            var applicationDbContext = (from f in _context.Payments where f.Wallets.WalletId == id select f).Include(i => i.Wallets).Include(i => i.Wallets.Levels).Include(i => i.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> Debts()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StudentsId;

            var student = (from s in _context.Students where s.Id == user select s).FirstOrDefault();
            var wallet = (from s in _context.UgSubWallets where s.WalletId == student.StudentId select s).Include(c => c.Sessions).ToList();
            
            
            if (wallet == null)
            {
                return RedirectToAction("pagenotfound", "error");
            }

            return View(wallet);

        }
        public async Task<IActionResult> Credit(string id)
        {
            TempData["Wallet"] = id;
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            ViewBag.name = (from s in _context.Students where s.Id == userId select s.SchoolEmailAddress).FirstOrDefault();
            return View();
        }

        // POST: order/Credit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Credit(CreditWallet creditWallet, UgMainWallet ugMainWallet)
        {
            
            Random r = new();
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
             
            creditWallet.Wallet = (string?)TempData["Wallet"];
            //creditWallet.StudentId = userId;
            creditWallet.Status = "Pending";
            creditWallet.PaymentDate = DateTime.Now;
            creditWallet.OrderId = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            _context.Add(creditWallet);
            await _context.SaveChangesAsync();
            return RedirectToAction("checkout","wallets", new {creditWallet.OrderId});
        }
        // GET: wallets/Checkout/5
        public async Task<IActionResult> Checkout(string? orderid)
        {
            var paymentToGet = await _context.Payments
                .FirstOrDefaultAsync(x => x.Ref == orderid);
            if (orderid == null || _context.Payments == null)
            {
                return NotFound();
            }
            if (paymentToGet == null)
            {
                return NotFound();
            }
            return View(paymentToGet);
        }

        public async Task<IActionResult> UpdateWallet(string data)
        {
            try
            {
                //This updates the status of the credit payment that was just made to approved
                var creditRow = (from c in _context.CreditWallets where c.OrderId == data select c).FirstOrDefault();
                creditRow.Status = "Approved";

                if (creditRow.Status == "Approved")
                {
                    //while this updates the credit column of the student wallet to the amount that was paid
                    var WalletCredit = await _context.UgMainWallets.Where(d => d.UTME == creditRow.UTME).FirstOrDefaultAsync();
                    WalletCredit.CreditBalance += creditRow.Amount;
                }
                await _context.SaveChangesAsync();
                //Tempdata doesnt have the capability to accept objects or to serialize objects.
                //As a result, you need to do this yourself
                TempData["PaymentAmount"] = JsonConvert.SerializeObject(creditRow.Amount);
                TempData["PaymentRef"] = creditRow.OrderId;
                TempData["PaymentDate"] = creditRow.PaymentDate;
                TempData["PaymentEmail"] = creditRow.Email;
               // TempData["PaymentWalletId"] = creditRow.Wallets.WalletId;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }
        ///Payment Receipt
        //public IActionResult Receipt()
        //{
        //    ViewBag.Amount = TempData["PaymentAmount"];
        //    ViewBag.PaymentRef = TempData["PaymentRef"];
        //    ViewBag.Date = TempData["PaymentDate"];
        //    ViewBag.Email = TempData["PaymentEmail"];
        //    ViewBag.WalletId = TempData["PaymentWalletId"];

        //    return View();
        //}


        /// <summary>
        /// /////////////////////////////////////////////////
        /// 
        /// 
        /// 
        /// Here Downwards is not being used!
        /// But it was kept incase we decide to start doing individual payments instead of internal paymenst
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: wallets/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id");
            ViewData["Session"] = new SelectList(_context.Sessions, "Id", "Id");
            return View();
        }

        // POST: wallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WalletId,FirstName,LastName,OtherName,UTME,CreditBalance,Debit,Status,DateCreated,Tuition,Tuition2,FortyPercent,SixtyPercent,LMS,AcceptanceFee,SRC,Email,DepartmentId,Session,Level")] UgMainWallet mainWallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainWallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(mainWallet);
        }

        // POST: wallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int id, [Bind("Id,WalletId,FirstName,LastName,OtherName,UTME,CreditBalance,Debit,Status,DateCreated,Tuition,Tuition2,FortyPercent,SixtyPercent,LMS,AcceptanceFee,SRC,Email,DepartmentId,Session,Level")] UgMainWallet mainWallet)
        {
            if (id != mainWallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainWallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyWalletExists(mainWallet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(mainWallet);
        }

        // GET: wallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UgSubWallets == null)
            {
                return NotFound();
            }

            //var myWallet = await _context.UgSubWallets
            //    .Include(m => m.Applicants)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (myWallet == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UgSubWallets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UgSubWallets'  is null.");
            }
            var subWallet = await _context.UgSubWallets.FindAsync(id);
            if (subWallet != null)
            {
                _context.UgSubWallets.Remove(subWallet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Info()
        {
            return View();
        }
        private bool MyWalletExists(int? id)
        {
            return _context.UgSubWallets.Any(e => e.Id == id);
        }

        //////////////////////////////////////////////////////////////
        ////////////////////TRANSACTION MODULES//////////////////////
        [Authorize(Roles = "student, superAdmin")]
        //Initiating Acceptance payment
        public async Task<IActionResult> Acceptance(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.AcceptanceFee;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "Acceptance";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating Tuition payment
        public async Task<IActionResult> TuitionTransfer(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.Tuition2;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "Tuition(Transfer)";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //[Authorize(Roles = "student, superAdmin")]
        //Initiating Tuition payment
        public async Task<IActionResult> Tuition(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.Tuition;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "Tuition";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating Tuition 60 Percent payment
        public async Task<IActionResult> Tuition60(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.SixtyPercent;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "Tuition(60%)";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating Tuition 40 Percent payment
        public async Task<IActionResult> Tuition40(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.FortyPercent;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "Tuition(40%)";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating LMS payment
        public async Task<IActionResult> LMS(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.LMS;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "LMS";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating SRC payment
        public async Task<IActionResult> SRC(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.SRC;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "SRC";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating EDHIS payment
        public async Task<IActionResult> EDHIS(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Amount = (double)wallet.EDHIS;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            payment.Type = "EDHIS";
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            //Get the payment to return
            var paymentToGet = await _context.Payments
                .FindAsync(payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [Authorize(Roles = "student, superAdmin")]
        //GET: Other Payments
        //Initiating Other payments
        public async Task<IActionResult> Hostel(string id, HostelPayment payment)
        {
            //Using Viewbag to display list of other fees and session from their respective tables table.

            ViewData["Hostel"] = new SelectList(_context.Hostels, "Id", "Name");

            var wallet = await _context.UgSubWallets
                .FirstOrDefaultAsync(m => m.WalletId == id);
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StudentsId;

            Random r = new();
            //Payment is created just before it returns the view
            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            _context.HostelPayments.Add(payment);
            await _context.SaveChangesAsync();
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint and the post method to update record
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["WalletId"] = wallet.Id;

            return View(payment);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating Hostel Payments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Hostel(string Ref)
        {
            try
            {
                var PaymentToUpdate = await _context.HostelPayments.FirstOrDefaultAsync(i => i.Ref == Ref);

                if (await TryUpdateModelAsync<HostelPayment>(PaymentToUpdate, "", c => c.Email, c => c.HostelType))
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                        var hostelamount = (from o in _context.Hostels where o.Id == PaymentToUpdate.HostelType select o.Amount).Sum();
                        PaymentToUpdate.Amount = (double?)hostelamount;
                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction("HostelCheckout", "wallets", new { Ref });

                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> HostelCheckout(string Ref)
        {

            var paymentToUpdate = _context.HostelPayments.Where(i => i.Ref == Ref).Include(i => i.HostelFees).FirstOrDefault();

            if (Ref == null || _context.HostelPayments == null)
            {
                return NotFound();
            }
            if (paymentToUpdate == null)
            {
                return NotFound();
            }

            return View(paymentToUpdate);
        }
        [Authorize(Roles = "student, superAdmin")]
        //GET: Other Payments
        //Initiating Other payments
        public async Task<IActionResult> Others(string id, Payment payment, Student student)
        {
            //Using Viewbag to display list of other fees and session from their respective tables table.
            ViewData["otherFees"] = new SelectList(_context.OtherFees, "Id", "Name");

            var wallet = await _context.UgSubWallets
                .FirstOrDefaultAsync(m => m.WalletId == id);
            Random r = new();
            //Payment is created just before it returns the view

            ViewBag.Name = wallet.Name;
            payment.SessionId = wallet.SessionId;
            payment.WalletId = wallet.Id;
            payment.Status = "Pending";
            payment.Ref = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
            payment.PaymentDate = DateTime.Now;
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint and the post method to update record
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["WalletId"] = wallet.Id;

            return View(payment);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Initiating Other Payments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Others(string Ref)
        {
            try
            {
                var PaymentToUpdate = await _context.Payments.FirstOrDefaultAsync(x => x.Ref == Ref);
                //var OtherRef = Ref;

                if (await TryUpdateModelAsync<Payment>(PaymentToUpdate, "", c => c.Email, c => c.OtherFeesDesc))
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                        var othersText = (from o in _context.OtherFees where o.Id == PaymentToUpdate.OtherFeesDesc select o.Amount).Sum();
                        PaymentToUpdate.Amount = (double?)othersText;
                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction("otherscheckout", "wallets", new { Ref });

                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> OthersCheckout(string Ref)
        {
            var paymentToUpdate = _context.Payments.Where(i => i.Ref == Ref).FirstOrDefault();
            if (Ref == null || _context.Payments == null)
            {
                return NotFound();
            }
            if (paymentToUpdate == null)
            {
                return NotFound();
            }

            return View(paymentToUpdate);
        }
        [Authorize(Roles = "student, superAdmin")]
        //Proceed to payment Gateway
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEmail(string? Ref)
        {
            try
            {
                //var order = (from x in _context.Payments where x.Ref == Ref select x.Wallets.WalletId).FirstOrDefault();

                var PaymentToUpdate = await _context.Payments
               .FirstOrDefaultAsync(c => c.Ref == Ref);
                var orderid = Ref;
                if (await TryUpdateModelAsync<Payment>(PaymentToUpdate, "", c => c.Email))
                {

                    try
                    {
                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction("checkout", "wallets", new { orderid });

                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        //[Authorize(Roles = "student, superAdmin")]
        //This is for returning student as the wallet activation for fresh students is done in the applicant controller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateWallets( UgSubWallet subWallet, Applicant applicant)
        {

            var session = (from s in _context.Sessions where s.IsActive == true select s).ToList();
           
            //Since this module only activates wallet for freshers, regardless of the level
            //you're admitted to, you'd pay the amount the 100l are paying for that session
            var fee = (from t in _context.AllFees
                              where t.DepartmentId == applicant.AdmittedInto &&
                              t.Sessions.Id == applicant.YearOfAdmission
                              select t).FirstOrDefault();

            subWallet.LMS = fee.LMS;
            subWallet.SRC = fee.SRC;

            subWallet.Tuition = fee.Tuition;
            if (applicant.ModeOfEntry == "Transfer")
            {
                subWallet.Tuition2 = subWallet.Tuition;
            }
            subWallet.Debit = subWallet.Tuition + subWallet.Tuition2 + subWallet.LMS + subWallet.SRC;
            subWallet.CreditBalance = 0;
            subWallet.WalletId = applicant.UTMENumber;
            subWallet.DateCreated = DateTime.Now;

            subWallet.Status = true;
            _context.UgSubWallets.Add(subWallet);

            //////////////////////////////////
            ///
            var wallet = await _context.UgMainWallets.FindAsync(subWallet.WalletId);

            if (subWallet.WalletId == null)
            {
                return NotFound();
            }

            try
            {

                var WalletToUpdate = await _context.UgMainWallets
                .FirstOrDefaultAsync(c => c.WalletId == subWallet.WalletId);

                if (await TryUpdateModelAsync<UgMainWallet>(WalletToUpdate, ""))
                {
                    wallet.BulkDebitBalanace = subWallet.Debit;
                    wallet.Status = true;
                    wallet.DateUpdated = DateTime.Now;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction("Index", "Wallet");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> HostelUpdate(string data, BursaryClearance bursaryClearance)
        {
            try
            {
                var hostelPaymentToUpdate = _context.HostelPayments.Where(x => x.Ref == data).Include(x => x.HostelFees).FirstOrDefault();
                var session = (from s in _context.Sessions where s.Id == hostelPaymentToUpdate.SessionId select s).FirstOrDefault();
                var wlt = (from e in _context.UgSubWallets where e.Id == hostelPaymentToUpdate.WalletId select e).FirstOrDefault();
                var department = (from d in _context.Departments where d.Id == wlt.Department select d.Name).FirstOrDefault();

                hostelPaymentToUpdate.Status = "Approved";
                hostelPaymentToUpdate.ReceiptNo = "BSA-" + DateTime.Now.Year.ToString().Substring(2);
                _context.SaveChangesAsync();


                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                if (loggedInUser != null)
                {
                    var user = loggedInUser.StudentsId;
                    bursaryClearance.StudentId = user;
                }
                else
                {
                    //bursaryClearance.StudentId = ;
                }
                bursaryClearance.ClearanceId = Guid.NewGuid().ToString();
                bursaryClearance.HostelId = hostelPaymentToUpdate.Id;
                bursaryClearance.SessionId = session.Id;
                bursaryClearance.CreatedAt = DateTime.Now;
                _context.BursaryClearances.Add(bursaryClearance);
                await _context.SaveChangesAsync();

                TempData["PaymentSession"] = session.Name;
                TempData["PaymentRef"] = hostelPaymentToUpdate.Ref;
                TempData["ReceiptNo"] = hostelPaymentToUpdate.ReceiptNo;
                TempData["PaymentDate"] = hostelPaymentToUpdate.PaymentDate;
                TempData["PaymentDepartment"] = department;
                TempData["PaymentUTME"] = wlt.RegNo;
                TempData["PaymentName"] = wlt.Name;
                TempData["PaymentEmail"] = hostelPaymentToUpdate.Email;
                //Tempdata doesnt have the capability to accept objects or to serialize objects.
                //As a result, you need to do this yourself
                TempData["PaymentAmount"] = JsonConvert.SerializeObject(hostelPaymentToUpdate.Amount);
                TempData["PaymentDescription"] = hostelPaymentToUpdate.HostelFees.Name;
                TempData["PaymentWalletId"] = wlt.WalletId;
                return RedirectToAction("Index", "Wallets");
            }
            catch (Exception)
            {

                throw;
            }


        }
      //  [Authorize(Roles = "student, superAdmin")]
        //Updating the payment record and creating tempdata for receipt
        public async Task<IActionResult> UpdatePayment(string data, BursaryClearance bursaryClearance)
        {

            var walletId = TempData["walletId"];
            //var paymentDescription = payments.Type;

            var payments = await _context.Payments.FirstOrDefaultAsync(c => c.Ref == data);
            if (await TryUpdateModelAsync<Payment>(payments, ""))
            {
                payments.Status = "Approved";

                switch (payments.Type)
                {
                    case "Tuition(Transfer)":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.Tuition2;
                            wallet.Debit = newDebit;

                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.Tuition2;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.Tuition2 = 0;
                        }
                        break;
                    case "Tuition":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.Tuition;
                            wallet.Debit = newDebit;

                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.Tuition;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.Tuition = 0;

                            wallet.SixtyPercent = 0;
                            wallet.FortyPercent = 0;
                        }
                        break;
                    case "Tuition(60%)":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.SixtyPercent;
                            wallet.Debit = newDebit;
                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.SixtyPercent;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.SixtyPercent = 0;
                            wallet.Tuition = 0;
                        }
                        break;
                    case "Tuition(40%)":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.FortyPercent;
                            wallet.Debit = newDebit;

                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.FortyPercent;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.FortyPercent = 0;
                        }
                        break;
                    case "EDHIS":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.EDHIS;
                            wallet.Debit = newDebit;
                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.EDHIS;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.EDHIS = 0;
                        }
                        break;
                    case "LMS":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.LMS;
                            wallet.Debit = newDebit;

                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.LMS;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.LMS = 0;
                        }
                        break;
                    case "SRC":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.SRC;
                            wallet.Debit = newDebit;

                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.SRC;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.SRC = 0;
                        }
                        break;
                    case "Acceptance":
                        if (payments.Status == "Approved")
                        {
                            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newDebit = wallet.Debit - wallet.AcceptanceFee;
                            wallet.Debit = newDebit;

                            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
                            var newBulkDebit = bulkwallet.BulkDebitBalanace - wallet.AcceptanceFee;
                            bulkwallet.BulkDebitBalanace = newBulkDebit;

                            wallet.AcceptanceFee = 0;
                        }
                        break;
                    await _context.SaveChangesAsync();
                 }
                var session = (from s in _context.Sessions where s.Id == payments.SessionId select s).FirstOrDefault();
                var wlt = (from e in _context.UgSubWallets where e.Id == payments.WalletId select e).FirstOrDefault();
                var department = (from d in _context.Departments where d.Id == wlt.Department select d.Name).FirstOrDefault();
                //////////////////////////////////////////////
                // CREATING BURSARY CLEARANCE WITH THE JUST MADE PAYMENT
                /////////////////////////////////////////////
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                if (loggedInUser != null)
                {
                    var user = loggedInUser.StudentsId;
                    bursaryClearance.StudentId = user;
                }
                else
                {
                    //bursaryClearance.StudentId = ;
                }
                bursaryClearance.ClearanceId = Guid.NewGuid().ToString();
                bursaryClearance.PaymentId = payments.Id;
                bursaryClearance.SessionId = session.Id;
                bursaryClearance.CreatedAt = DateTime.Now;
                _context.BursaryClearances.Add(bursaryClearance);
                await _context.SaveChangesAsync();


                TempData["PaymentSession"] = session.Name;
                TempData["PaymentRef"] = payments.Ref;
                TempData["ReceiptNo"] = "BSA-" + payments.Id + "-" + DateTime.Now.Year;
                TempData["PaymentDate"] = payments.PaymentDate;
                TempData["PaymentDepartment"] = department;
                TempData["PaymentUTME"] = wlt.RegNo;
                TempData["PaymentName"] = wlt.Name;
                TempData["PaymentEmail"] = payments.Email;
                //Tempdata doesnt have the capability to accept objects or to serialize objects.
                //As a result, you need to do this yourself
                TempData["PaymentAmount"] = JsonConvert.SerializeObject(payments.Amount);
                //TempData["PaymentDescription"] = TempData["PaymentDescription1"];
                TempData["PaymentWalletId"] = wlt.WalletId;
            }

            return RedirectToAction("Index", "Wallets");
        }

        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> Verify(string? id)
        {
            if (id == null || _context.Payments == null)
            {
                return RedirectToAction("PageNotFound", "error");
            }

            var payment = (from p in _context.Payments where p.Ref == id select p).FirstOrDefault();
            if (payment == null || payment.Status == "Pending")
            {
                return RedirectToAction("BadReq", "error");
            }
            Random r = new();
            var session = (from s in _context.Sessions where s.Id == payment.SessionId select s.Name).FirstOrDefault();
            var wlt = (from e in _context.UgSubWallets where e.Id == payment.WalletId select e).FirstOrDefault();
            var department = (from d in _context.Departments where d.Id == wlt.Department select d.Name).FirstOrDefault();
            TempData["PaymentSession"] = session;
            TempData["PaymentRef"] = payment.Ref;
            TempData["ReceiptNo"] = "BSA-" + DateTime.Now.Year.ToString().Substring(2) + "-" + payment.Id;
            TempData["PaymentDate"] = payment.PaymentDate;
            TempData["PaymentDepartment"] = department;
            TempData["PaymentUTME"] = wlt.RegNo;
            TempData["PaymentName"] = wlt.Name;
            TempData["PaymentEmail"] = payment.Email;
            //Tempdata doesnt have the capability to accept objects or to serialize objects.
            //As a result, you need to do this yourself
            TempData["PaymentAmount"] = JsonConvert.SerializeObject(payment.Amount);
            TempData["PaymentDescription"] = payment.Type;
            TempData["PaymentWalletId"] = wlt.WalletId;
            return RedirectToAction("receipt", new { id });
            //return View(payment);
        }
        [Authorize(Roles = "student, superAdmin")]

        //Payment Receipt
        public IActionResult Receipt()
        {
            //var d = _context.HostelPayments.Where(x => x.Ref == Ref).FirstOrDefault();
            ViewBag.PaymentRef = TempData["PaymentRef"];
            ViewBag.ReceiptNo = TempData["ReceiptNo"];
            ViewBag.Date = TempData["PaymentDate"];
            ViewBag.Name = TempData["PaymentName"];
            ViewBag.Email = TempData["PaymentEmail"];
            ViewBag.UTME = TempData["PaymentUTME"];
            ViewBag.Department = TempData["PaymentDepartment"];
            ViewBag.Session = TempData["PaymentSession"];
            ViewBag.Amount = TempData["PaymentAmount"];
            ViewBag.Description = TempData["PaymentDescription"];
            ViewBag.WalletId = TempData["PaymentWalletId"];

            Console.WriteLine("update receipt with " + ViewBag.PaymentRef);
            return View();
        }

    }
}
