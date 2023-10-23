using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages
{
    public class ProducaoModel : PageModel
    {
        public List<Entities.Producao> listaProducao = new List<Entities.Producao>();
        public List<Entities.Plataforma> listaPlataformas = new List<Entities.Plataforma>();
        public int codigoPlataforma = 0;

        public void OnGet()
        {
            var clsProducao = new Entities.Producao();
            var clsPlataforma = new Entities.Plataforma();

            try
            {
                codigoPlataforma = int.Parse(Request.Query["codigo_plataforma"]);
            }
            catch
            {
                codigoPlataforma = 0;
            }

            listaProducao = clsProducao.ListarProducao(0, codigoPlataforma);
            listaPlataformas = clsPlataforma.ListarPlataformas(0, 0, "");
        }

        public void OnPost()
        {
            var clsProducao = new Entities.Producao();
            var clsPlataforma = new Entities.Plataforma();

            codigoPlataforma = int.Parse(Request.Form["codigoPlataforma"]);

            listaProducao = clsProducao.ListarProducao(0, codigoPlataforma);
            listaPlataformas = clsPlataforma.ListarPlataformas(0, 0, "");
        }

    }
}
