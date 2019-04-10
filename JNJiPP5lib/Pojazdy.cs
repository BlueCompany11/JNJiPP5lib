using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-	Utworzyć zestaw komponentów współpracujących ze sobą. to calosc jest komponentem
namespace JNJiPP5lib
{
    public abstract class PojazdKomunikacjiMiejskiej //		Abstrakcja -utworzyć i zastosować klasę abstrakcyjną 
    {
        //	Hermetyzacja klasy – utworzyć właściwości i określić poziomy dostępu dla składowych klasy – zabezpieczyć pola za pomocą właściwości
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
        // koniec wlasciwosci
        //	Polimorfizm (przeciążanie, nadpisywanie metod) - zastosować metody wirtualne, zaprojektować metody przeciążone, zastosować nadpisywanie metod wirtualnych
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
        //udostepniam z poziomu komponentu klase ktora ma publiczne zdarzenie 
        public event Action PrzekroczenieBezpiecznejPredkosci;
        //public event A PrzekroczenieBezpiecznejPredkosci;
        //public delegate void A();
        public event Action ZniszczeniePojazdu;
    }
    //	Dziedziczenie – świadomie zastosować dziedziczenie.
    public class Autobus : PojazdKomunikacjiMiejskiej
    {
        protected override int miejscaSiedziace => 40;

        protected override int miejscaStojace => 70;

        protected override int maxPredkosc => 90;

        public override void ZmienPredkosc(double ile) //zastosować nadpisywanie metod wirtualnych
        {
            aktualnaPredkoscPojazdu += ile;
            if (aktualnaPredkoscPojazdu >= 0.7 * (maxPredkosc))
            {
                WyowlajZdarzeniePrzekroczeniaPredkosci();
            }
        }
    }
    //	Dziedziczenie – świadomie zastosować dziedziczenie.
    public class Tramwaj : PojazdKomunikacjiMiejskiej
    {
        protected override int miejscaSiedziace => 50;

        protected override int miejscaStojace => 80;

        protected override int maxPredkosc => 60;
        
    }

}
