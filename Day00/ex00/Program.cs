using System;

namespace piscine_cs
{
    class Program
    {
        public static int DayPerYear (int year)
        {
            int i;
            int num;

            i = 1;
            num = 0;
            while (i <= 12)
                num = num + DateTime.DaysInMonth(year, i++);
            return (num);
        }

        public static double RepayWthSum(int term, int selectedMonth, double sum, double rate, double payment)
        {
            double  pay;
            double  i;
            double  degree;
            double  percent;
            double  OD;
            int     monthCount;
            int     monthNow;
            double  overpayment;
            string  date;
            int     year;

            year = DateTime.Today.Year;
            date = DateTime.Today.Year.ToString();
            monthNow = DateTime.Today.Month;
            monthCount = 1;
            OD = sum;
            i = rate/12/100;
            degree = Math.Pow(i + 1, term);
            pay = Math.Round(sum * i * degree / (degree - 1), 2);
            Console.WriteLine($" n {"Дата", 4} {"Платеж", -9}\t{"ОД", -15}\t{"Проценты", -14}\t{"Остаток долга", -17}");
            overpayment = 0;
            while (monthCount - 1 != term)
            {
                percent = Math.Round((sum * rate * DateTime.DaysInMonth(year, monthNow++))/ (100 * DayPerYear(DateTime.Now.Year)), 2);
                OD = Math.Round(pay - percent, 2);
                sum = Math.Round(sum - OD, 2);
                if (monthCount == selectedMonth)
                {
                    sum = sum - payment;
                    degree = Math.Pow(i + 1, term - selectedMonth);
                    pay = Math.Round(sum * i * degree / (degree - 1), 2);                   
                }
                if (sum < 0)
                    sum = 0;
                Console.WriteLine($"{monthCount, 2} {date} {pay, 9}р\t{OD:0.00}р\t{percent:0.00}р\t{sum:0.00,}р");
                monthCount++;
                if (monthNow == 13)
                {
                    monthNow = 1;
                    year++;
                }
                overpayment = Math.Round(overpayment + percent, 2);
            }
            Console.WriteLine($"Переплата в случае уменьшения платежа: {overpayment:0.00}р"); 
            return (overpayment);
        }
        public static double RepayWthTerm(int term, int selectedMonth, double sum, double rate, double payment)
        {
            double  pay;
            double  i;
            double  degree;
            double  percent;
            double  OD;
            int     monthCount;
            int     monthNow;
            double  overpayment;
            string  date;
            int     year;

            year = DateTime.Today.Year;
            date = DateTime.Today.Year.ToString();
            monthNow = DateTime.Today.Month;
            monthCount = 1;
            OD = sum;
            i = rate/12/100;
            degree = Math.Pow(i + 1, term);
            pay = Math.Round(sum * i * degree / (degree - 1), 2);
            Console.WriteLine($" n {"Дата", 4} {"Платеж", -9}\t{"ОД", -15}\t{"Проценты", -14}\t{"Остаток долга", -17}");
            overpayment = 0;
            while (monthCount - 1 != term)
            {
                percent = Math.Round((sum * rate * DateTime.DaysInMonth(year, monthNow++))/ (100 * DayPerYear(DateTime.Now.Year)), 2);
                OD = Math.Round(pay - percent, 2);
                sum = Math.Round(sum - OD, 2);
                if (monthCount == selectedMonth)
                    sum = sum - payment;
                if (sum < 0)
                    sum = 0;
                Console.WriteLine($"{monthCount, 2} {date} {pay, 9}р\t{OD:0.00}р\t{percent:0.00}р\t{sum:0.00,}р");
                monthCount++;
                if (monthNow == 13)
                {
                    monthNow = 1;
                    year++;
                }
                if (monthCount != term + 1)
                    overpayment = Math.Round(overpayment + percent, 2);
            }
            Console.WriteLine($"Переплата в случае уменьшения срока кредита: {overpayment:0.00}р");
            return (overpayment);
        }
        public static int CheckingArgCount(string[] args)
        {
            if (args.Length == 5)
                return (0);
            Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
            return (1);
        }
        public static int TryToParse(string[] str)
        {
            double  result_double;
            int     result_int;
            if ((
                double.TryParse(str[0], out result_double) &&
                double.TryParse(str[1], out result_double) &&
                int.TryParse(str[2], out result_int) &&
                int.TryParse(str[3], out result_int) &&
                double.TryParse(str[4], out result_double))
            )
                return (0);
            Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
            return (1);
        }
        static void Main(string[] args)
        {
            int     term, selectedMonth;
            double  sum, rate, payment, overpaymentTerm, overpaymentSum;


            if (CheckingArgCount(args) == 1)
                return;
            if (TryToParse(args) == 1)
                return;
            sum = double.Parse(args[0]);        //сумма кредита
            rate = double.Parse(args[1]);       //процентная ставка, %
            term = int.Parse(args[2]);          //количество месяцев
            selectedMonth = int.Parse(args[3]); //номер месяца досрочного платежа
            payment = double.Parse(args[4]);    //сумма досрочного платежа
            overpaymentTerm = RepayWthTerm(term, selectedMonth, sum, rate, payment);
            overpaymentSum = RepayWthSum(term, selectedMonth, sum, rate, payment);
            if (overpaymentSum < overpaymentTerm)
                Console.WriteLine($"Уменьшение платежа выгоднее уменьшения срока на {Math.Round(overpaymentTerm - overpaymentSum, 2)}р.");
            else if (overpaymentSum > overpaymentTerm)
                Console.WriteLine($"Уменьшение срока выгоднее уменьшения платежа на {overpaymentSum - overpaymentTerm:0.00}р.");
            else
                Console.WriteLine("Переплата одинакова в обоих вариантах.");
            return;
        }
    }
}
