using vahangpsapi.Context;

namespace vahangpsapi.Services
{
    public class GenerateVoucher
    {
        private readonly JwtContext _jwtContext;

        public GenerateVoucher(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }
        public string getvoucherno(int TrnId)
        {
            string voucherno = "";
            string BookID = "";
            if (TrnId == 1001)
            { BookID = "CR"; }
            if (TrnId == 1002)
            { BookID = "BR"; }
            var nextVoucherNo = _jwtContext.customer_payment.Where(x => x.payment_mode_id == TrnId).Count();
            nextVoucherNo = nextVoucherNo + 1;
            voucherno = BookID + nextVoucherNo;
            return voucherno;

        }
    }
}
