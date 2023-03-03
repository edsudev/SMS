using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    //This is class for individual wallets
    public class PgSubWallet
    {
        public int? Id { get; set; }
        [ForeignKey("Applicants")]
        public int? ApplicantId { get; set; }
        public Applicant? Applicants { get; set; }
        public string? WalletId { get; set; }
        public string? Name { get; set; }
        public string? Pic { get; set; }
        public string? RegNo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CreditBalance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Debit { get; set; }
        public bool? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        //All gets from fees table based on student dpt, lvl, session.
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Tuition { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FortyPercent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SixtyPercent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? LMS { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AcceptanceFee { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SRC { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? EDHIS { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        [ForeignKey("Levels")]
        public int? Level { get; set; }
        public Level? Levels { get; set; }
        [ForeignKey("Departments")]
        public int? Department { get; set; }
        public Department? Departments { get; set; }

    }
}
