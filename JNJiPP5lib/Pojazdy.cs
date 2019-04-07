using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNJiPP5lib
{
    public abstract class PojazdKomunikacjiMiejskiej
    {
        protected abstract int miejscaSiedziace { get; }
        protected abstract int miejscaStojace { get; }

        protected double aktualnaPredkoscPojazdu;
        protected abstract int maxPredkosc { get; }

        bool pojazdNadajeSieDoUzytku = true;

        protected int stanPojazdu;
        public int StanPojazdu {
            get { return stanPojazdu; }
            set { stanPojazdu += value;
                if (value < 0)
                {
                    throw new Exception("Nie mozna ustawic stanu pojazdu jako wartosc ujemna");
                }
                if (stanPojazdu >= 100)
                {
                    WyowlajZdarzenieZniszczeniePojazdu();
                }
            } }

        public virtual void ZmienPredkosc(double ile)
        {
            aktualnaPredkoscPojazdu += ile;
            if (aktualnaPredkoscPojazdu >= 0.75 * (maxPredkosc))
            {
                WyowlajZdarzeniePrzekroczeniaPredkosci();
            }
        }
        protected void WyowlajZdarzeniePrzekroczeniaPredkosci()
        {
            StanPojazdu += 40;
            if (StanPojazdu < 100)
            {
                Console.WriteLine("Uszkodzenie pojazdu");
                PrzekroczenieBezpiecznejPredkosci?.Invoke();//jezeli do zdarzenia nie ma przypisanej zadnej reakcji to sie nie wywolaj
            }
        }

        protected void WyowlajZdarzenieZniszczeniePojazdu()
        {
            if (pojazdNadajeSieDoUzytku == true)
            {
                Console.WriteLine("Zniszczenie pojazdu");
                ZniszczeniePojazdu?.Invoke();//jezeli do zdarzenia nie ma przypisanej zadnej reakcji to sie nie wywolaj
            }
            pojazdNadajeSieDoUzytku = false;

        }

        public event Action PrzekroczenieBezpiecznejPredkosci;
        //public event A PrzekroczenieBezpiecznejPredkosci;
        //public delegate void A();
        public event Action ZniszczeniePojazdu;
    }

    public class Autobus : PojazdKomunikacjiMiejskiej
    {
        protected override int miejscaSiedziace => 40;

        protected override int miejscaStojace => 70;

        protected override int maxPredkosc => 90;

        public override void ZmienPredkosc(double ile)
        {
            aktualnaPredkoscPojazdu += ile;
            if (aktualnaPredkoscPojazdu >= 0.7 * (maxPredkosc))
            {
                WyowlajZdarzeniePrzekroczeniaPredkosci();
            }
        }
    }

    public class Tramwaj : PojazdKomunikacjiMiejskiej
    {
        protected override int miejscaSiedziace => 50;

        protected override int miejscaStojace => 80;

        protected override int maxPredkosc => 60;
        
    }

}
