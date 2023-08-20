using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClases.Promociones;
using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using Toolkit.Core.CodeGen;


namespace ObligatiorioProg3_Estadio
{
    public class ListadePromos 
    {
      public FabricaPromociones ObtenerPromo(EventoPromocion ep)
        {
            if (ep == null) {
                return new SinPromocion();

            }
            else { 
            switch (Convert.ToUInt32(ep.promocion))
            {
                case 1:
                    return new Promo2X1();
                case 2:
                    return new PromoDescuento();
                case 3:
                    return new Promo2x1_Merienda_SOCIOS();
                default:
                    return new SinPromocion();
            }
            }
        }

          
     }
}



