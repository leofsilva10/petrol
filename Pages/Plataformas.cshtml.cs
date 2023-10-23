using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages
{
    public class PlataformasModel : PageModel
    {
        public List<Entities.Plataforma> listaPlataformas = new List<Entities.Plataforma>();
        public List<Entities.TipoPlataforma> listaTiposPlataformas = new List<Entities.TipoPlataforma>();
        public string nomePlataforma = "";
        public int codigoTipoPlataforma = 0;

        public void OnGet()
        {
            var clsPlataforma = new Entities.Plataforma();
            var clsTipoPlataforma = new Entities.TipoPlataforma();

            try
            {
                codigoTipoPlataforma = int.Parse(Request.Query["codigo_tipo"]);
            }
            catch
            {
                codigoTipoPlataforma = 0;
            }

            listaPlataformas = clsPlataforma.ListarPlataformas(0, codigoTipoPlataforma, "");
            listaTiposPlataformas = clsTipoPlataforma.ListarTiposPlataformas(0, "");
        }

        public void OnPost()
        {
            var clsPlataforma = new Entities.Plataforma();
            var clsTipoPlataforma = new Entities.TipoPlataforma();

            nomePlataforma = Request.Form["nomePlataforma"];
            codigoTipoPlataforma = int.Parse(Request.Form["codigoTipoPlataforma"]);

            listaPlataformas = clsPlataforma.ListarPlataformas(0, codigoTipoPlataforma, nomePlataforma);
            listaTiposPlataformas = clsTipoPlataforma.ListarTiposPlataformas(0, "");
        }

    }
}
