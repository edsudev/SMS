using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace EDSU_SYSTEM.Controllers
{
    public class orderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public orderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Credit()
        {
            ViewBag.Error = TempData["unsuccesfulPayment"];
            ViewBag.NullUTME = TempData["NoUTME"];


            return View();
        }
       
        // POST: order/Credit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Credit(CreditWallet creditWallet, UgMainWallet ugMainWallet)
        {
            var dataExists = _context.Students.Any(x => x.UTMENumber == creditWallet.UTME);

            if (dataExists)
            {
                Random r = new();
                //Check if the UTME Coming exists in the db

                creditWallet.Wallet = (string?)TempData["Wallet"];
                //creditWallet.UTME = userId;
                creditWallet.Status = "Pending";
                creditWallet.PaymentDate = DateTime.Now;
                creditWallet.OrderId = "EDSU-" + r.Next(10000000) + DateTime.Now.Millisecond;
                _context.Add(creditWallet);
                await _context.SaveChangesAsync();
                return RedirectToAction("checkout", "wallets", new { creditWallet.OrderId });
            }
            else
            {
                TempData["NoUTME"] = "The UTME Number provide does not have a record. Contact admin if problem persist.";
                return View();
            }
        }
        // GET: order
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UgSubWallets.Include(u => u.Applicants).Include(u => u.Departments).Include(u => u.Levels).Include(u => u.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UgSubWallets == null)
            {
                return NotFound();
            }

            var ugSubWallet = await _context.UgSubWallets
                .Include(u => u.Applicants)
                .Include(u => u.Departments)
                .Include(u => u.Levels)
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ugSubWallet == null)
            {
                return NotFound();
            }

            return View(ugSubWallet);
        }

        // GET: order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UgSubWallets == null)
            {
                return NotFound();
            }

            var ugSubWallet = await _context.UgSubWallets.FindAsync(id);
            if (ugSubWallet == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.UgApplicants, "Id", "Id", ugSubWallet.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", ugSubWallet.Department);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", ugSubWallet.Level);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", ugSubWallet.SessionId);
            return View(ugSubWallet);
        }

        // POST: order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,WalletId,ApplicantId,Name,Pic,RegNo,CreditBalance,Debit,Status,DateCreated,Tuition,Tuition2,FortyPercent,SixtyPercent,LMS,AcceptanceFee,SRC,EDHIS,SessionId,Level,Department")] UgSubWallet ugSubWallet)
        {
            if (id != ugSubWallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ugSubWallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UgSubWalletExists(ugSubWallet.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.UgApplicants, "Id", "Id", ugSubWallet.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", ugSubWallet.Department);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", ugSubWallet.Level);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", ugSubWallet.SessionId);
            return View(ugSubWallet);
        }

        // GET: order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UgSubWallets == null)
            {
                return NotFound();
            }

            var ugSubWallet = await _context.UgSubWallets
                .Include(u => u.Applicants)
                .Include(u => u.Departments)
                .Include(u => u.Levels)
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ugSubWallet == null)
            {
                return NotFound();
            }

            return View(ugSubWallet);
        }

        // POST: order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.UgSubWallets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UgSubWallets'  is null.");
            }
            var ugSubWallet = await _context.UgSubWallets.FindAsync(id);
            if (ugSubWallet != null)
            {
                _context.UgSubWallets.Remove(ugSubWallet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UgSubWalletExists(int? id)
        {
          return (_context.UgSubWallets?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        //////////////////////////////////////////////////////////////
        ////////////////////TRANSACTION MODULES//////////////////////
        public async Task<IActionResult> Acceptance(string id, Payment payment)
        {
            //In this method, a payment row is created just before the
            //page is returned and the id is used to return the payment to the page.

            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);
            Random r = new();
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

            /////////////////////////////////////////////

            var paymentToGet =  await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x=>x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //Initiating Tuition payment
        public async Task<IActionResult> TuitionTransfer(string id, Payment payment)
        {
            //In this method, a payment row is created just before the
            //page is returned and the id is used to return the payment to the page.
            var wallet = await _context.UgSubWallets
                .FirstOrDefaultAsync(m => m.WalletId == id);
            Random r = new();
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
            /////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //Initiating Tuition payment
        public async Task<IActionResult> Tuition(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
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
            ////////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }

        //Initiating Tuition 60 Percent payment
        public async Task<IActionResult> Tuition60(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);
            Random r = new();
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
            //////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //Initiating Tuition 40 Percent payment
        public async Task<IActionResult> Tuition40(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
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
            /////////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //Initiating LMS payment
        public async Task<IActionResult> LMS(string id, Payment payment, Student student)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
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
            ///////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //Initiating SRC payment
        public async Task<IActionResult> SRC(string id, Payment payment)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
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

            //////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                 .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        //Initiating EDHIS payment
        public async Task<IActionResult> EDHIS(string id, Payment payment)
        {
            var wallet = await _context.UgSubWallets
                 .FirstOrDefaultAsync(m => m.WalletId == id);
            Random r = new();
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
            //////////////////////////////////////////////////
            var paymentToGet = await _context.Payments.Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == payment.Id);
            if (paymentToGet == null)
            {
                return NotFound();
            }
            //When the payment row is created, it stores the id in a tempdata then pass it to the verify endpoint
            TempData["PaymentId"] = payment.Wallets.WalletId;
            TempData["walletId"] = id;
            return View(paymentToGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Updating the payment record and creating tempdata for receipt
        public async Task<IActionResult> UpdatePayment(string Ref, BursaryClearance bursaryClearance)
        {
            var walletId = TempData["walletId"];
            var payments = await _context.Payments.FirstOrDefaultAsync(c => c.Ref == Ref);
            var wallet = await _context.UgSubWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
            if ((int)bulkwallet.CreditBalance >= payments.Amount)
            {
                payments.Status = "Approved";
                switch (payments.Type)
                {
                    case "Tuition(Transfer)":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.Tuition2;
                            bulkwallet.BulkDebitBalanace -= wallet.Tuition2;
                            bulkwallet.CreditBalance -= wallet.Tuition2;
                            wallet.Tuition2 = 0;
                        }
                        break;
                    case "Tuition":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.Tuition;
                            bulkwallet.BulkDebitBalanace -= wallet.Tuition;
                            bulkwallet.CreditBalance -= wallet.Tuition;
                            wallet.Tuition = 0;
                            wallet.SixtyPercent = 0;
                            wallet.FortyPercent = 0;
                        }
                        break;
                    case "Tuition(60%)":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.SixtyPercent;
                            bulkwallet.BulkDebitBalanace -= wallet.SixtyPercent;
                            bulkwallet.CreditBalance -= wallet.SixtyPercent;
                            wallet.SixtyPercent = 0;
                            wallet.Tuition = 0;
                        }
                        break;
                    case "Tuition(40%)":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.FortyPercent;
                            bulkwallet.BulkDebitBalanace -= wallet.FortyPercent;
                            bulkwallet.CreditBalance -= wallet.FortyPercent;
                            wallet.FortyPercent = 0;
                        }
                        break;
                    case "EDHIS":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.EDHIS;
                            bulkwallet.BulkDebitBalanace -= wallet.EDHIS;
                            bulkwallet.CreditBalance -= wallet.EDHIS;
                            wallet.EDHIS = 0;
                        }
                        break;
                    case "LMS":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.LMS;
                            bulkwallet.BulkDebitBalanace -= wallet.LMS;
                            bulkwallet.CreditBalance -= wallet.LMS;
                            wallet.LMS = 0;
                        }
                        break;
                    case "SRC":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.SRC;
                            bulkwallet.BulkDebitBalanace -= wallet.SRC;
                            bulkwallet.CreditBalance -= wallet.SRC;
                            wallet.SRC = 0;
                        }
                        break;
                    case "Acceptance":
                        if (payments.Status == "Approved")
                        {
                            wallet.Debit -= wallet.AcceptanceFee;
                            bulkwallet.BulkDebitBalanace -= wallet.AcceptanceFee;
                            bulkwallet.CreditBalance -= wallet.AcceptanceFee;
                            wallet.AcceptanceFee = 0;
                        }
                        break;
               }
                var session = (from s in _context.Sessions where s.Id == payments.SessionId select s).FirstOrDefault();
                var wlt = (from e in _context.UgSubWallets where e.Id == payments.WalletId select e).FirstOrDefault();
                var department = (from d in _context.Departments where d.Id == wlt.Department select d.Name).FirstOrDefault();
                //////////////////////////////////////////////
                // CREATING BURSARY CLEARANCE WITH THE JUST MADE PAYMENT
                /////////////////////////////////////////////
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var user = loggedInUser.StudentsId;
                bursaryClearance.StudentId = user;
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
                TempData["PaymentWalletId"] = wlt.WalletId;
            }
            TempData["unsuccesfulPayment"] = "You do not have sufficient credit to complete the transaction!";
            return RedirectToAction("credit", "order");
        }

        //////////////////////////////////////////////////////////////
        ///////////////////HOSTEL PAYMENT ENDPOINT////////////////////
        public async Task<IActionResult> Hostel(string id, HostelPayment payment)
        {
            ViewData["Hostel"] = new SelectList(_context.Hostels, "Id", "Name");

            var wallet = await _context.UgSubWallets
                .FirstOrDefaultAsync(m => m.WalletId == id);

            Random r = new();
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
            TempData["WalletId"] = wallet.WalletId;

            return View(payment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Hostel(string Ref)
        {
            try
            {
                var PaymentToUpdate = await _context.HostelPayments.FirstOrDefaultAsync(i => i.Ref == Ref);

                if (await TryUpdateModelAsync<HostelPayment>(PaymentToUpdate, "", c => c.HostelType))
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
                    return RedirectToAction("HostelCheckout", "order", new { Ref });

                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        public async Task<IActionResult> HostelCheckout(string Ref)
        {

            var paymentToUpdate = _context.HostelPayments.Where(i => i.Ref == Ref).Include(i => i.HostelFees).Include(i => i.Sessions).FirstOrDefault();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Updating the payment record and creating tempdata for receipt
        public async Task<IActionResult> UpdateHostelPayment(string Ref, BursaryClearance bursaryClearance)
        {
            var walletId = TempData["walletId"];
            var payments = await _context.HostelPayments.FirstOrDefaultAsync(c => c.Ref == Ref);
            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
            if ((int)bulkwallet.CreditBalance >= payments.Amount)
            {
                payments.Status = "Approved";
                
                var session = (from s in _context.Sessions where s.Id == payments.SessionId select s).FirstOrDefault();
                var wlt = (from e in _context.UgSubWallets where e.Id == payments.WalletId select e).FirstOrDefault();
                var department = (from d in _context.Departments where d.Id == wlt.Department select d.Name).FirstOrDefault();
                //////////////////////////////////////////////
                // CREATING BURSARY CLEARANCE WITH THE JUST MADE PAYMENT
                /////////////////////////////////////////////
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var user = loggedInUser.StudentsId;
                bursaryClearance.StudentId = user;
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
                TempData["PaymentWalletId"] = wlt.WalletId;
            }
            TempData["unsuccesfulPayment"] = "You do not have sufficient credit to complete the transaction!";
            return RedirectToAction("credit", "order");
        }

        //////////////////////////////////////////////////////////////
        ///////////////////OTHER PAYMENTS ENDPOINT////////////////////
        public async Task<IActionResult> Others(string id, Payment payment, Student student)
        {
            //Using Viewbag to display list of other fees and session from their respective tables table.
            ViewData["otherFees"] = new SelectList(_context.OtherFees, "Id", "Name");

            var wallet = await _context.UgSubWallets
                .FirstOrDefaultAsync(m => m.WalletId == id);
            Random r = new();
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
            TempData["WalletId"] = wallet.WalletId;

            return View(payment);
        }
        //Initiating Other Payments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Others(string Ref)
        {
            try
            {
                var PaymentToUpdate = await _context.Payments.FirstOrDefaultAsync(x => x.Ref == Ref);

                if (await TryUpdateModelAsync<Payment>(PaymentToUpdate, "", c => c.OtherFeesDesc))
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
                    return RedirectToAction("otherscheckout", "order", new { Ref });

                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        public async Task<IActionResult> OthersCheckout(string Ref)
        {
            var paymentToUpdate = _context.Payments.Where(i => i.Ref == Ref).Include(i => i.OtherFees).Include(i => i.Sessions).FirstOrDefault();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        //Updating the payment record and creating tempdata for receipt
        public async Task<IActionResult> UpdateOtherPayment(string Ref, BursaryClearance bursaryClearance)
        {
            var walletId = TempData["walletId"];
            var payments = await _context.Payments.FirstOrDefaultAsync(c => c.Ref == Ref);
            var bulkwallet = await _context.UgMainWallets.FirstOrDefaultAsync(i => i.WalletId == walletId);
            if ((int)bulkwallet.CreditBalance >= payments.Amount)
            {
                payments.Status = "Approved";

                var session = (from s in _context.Sessions where s.Id == payments.SessionId select s).FirstOrDefault();
                var wlt = (from e in _context.UgSubWallets where e.Id == payments.WalletId select e).FirstOrDefault();
                var department = (from d in _context.Departments where d.Id == wlt.Department select d.Name).FirstOrDefault();
                //////////////////////////////////////////////
                // CREATING BURSARY CLEARANCE WITH THE JUST MADE PAYMENT
                /////////////////////////////////////////////
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var user = loggedInUser.StudentsId;
                bursaryClearance.StudentId = user;
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
                TempData["PaymentWalletId"] = wlt.WalletId;
            }
            TempData["unsuccesfulPayment"] = "You do not have sufficient credit to complete the transaction!";
            return RedirectToAction("credit", "order");
        }
        //////////////////////////////////////////////////////////////
        ///////////////////VERIFY PAYMENT ENDPOINT////////////////////
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

        }
        ///Payment Receipt
        public IActionResult Receipt()
        {
            ViewBag.Amount = TempData["PaymentAmount"];
            ViewBag.PaymentRef = TempData["PaymentRef"];
            ViewBag.Date = TempData["PaymentDate"];
            ViewBag.Email = TempData["PaymentEmail"];
            ViewBag.WalletId = TempData["PaymentWalletId"];

            return View();
        }


    }
}
