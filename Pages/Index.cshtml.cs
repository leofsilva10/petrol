using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Entities.TipoPlataforma> listaTiposPlataformas = new List<Entities.TipoPlataforma>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var clsTipoPlataforma = new Entities.TipoPlataforma();

            listaTiposPlataformas = clsTipoPlataforma.ListarTiposPlataformas(0, "");
        }
    }
}