using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages
{
    public class TiposPlataformasModel : PageModel
    {
        public List<Entities.TipoPlataforma> listaTiposPlataformas = new List<Entities.TipoPlataforma>();
        public string nomeTipoPlataforma = "";

        public void OnGet()
        {
            var clsTipoPlataforma = new Entities.TipoPlataforma();

            listaTiposPlataformas = clsTipoPlataforma.ListarTiposPlataformas(0, "");
        }

        public void OnPost()
        {
            var clsTipoPlataforma = new Entities.TipoPlataforma();

            nomeTipoPlataforma = Request.Form["nomeTipoPlataforma"];

            listaTiposPlataformas = clsTipoPlataforma.ListarTiposPlataformas(0, nomeTipoPlataforma);
        }
    }
}
