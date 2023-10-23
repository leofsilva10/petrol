using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages.Plataformas
{
    public class CreateModel : PageModel
    {
        public Entities.Plataforma infoPlataforma = new Entities.Plataforma();
        public List<Entities.TipoPlataforma> listaTiposPlataformas = new List<Entities.TipoPlataforma>();
        public string msgErro = "";
        public string msgSucesso = "";
        public void OnGet()
        {
            var classTipoPlataforma = new Entities.TipoPlataforma();
            
            //Preencher a lista de tipos de plataformas.
            listaTiposPlataformas = classTipoPlataforma.ListarTiposPlataformas(0, "");
        }

        public void OnPost()
        {
            // Obter dados informados pelo usuário.

            infoPlataforma.Nome = Request.Form["nome"];
            infoPlataforma.CodigoTipo = int.Parse(Request.Form["tipo"]);

            // Verificar se os dados foram cadastrados corretamente.

            if (infoPlataforma.Nome.Length == 0 || infoPlataforma.CodigoTipo == 0)
            {
                msgErro = "Favor preencher todos os dados.";
                return;
            }

            // Adicionar a nova plataforma.

            try
            {
                var classPlataforma = new Entities.Plataforma();

                if (classPlataforma.Adicionar(infoPlataforma) == "0")
                {
                    msgErro = "Erro ao adicionar registro.";
                    return;
                }
            }
            catch (Exception ex)
            {
                msgErro = ex.Message;
                return;
            }

            infoPlataforma.Nome = "";
            infoPlataforma.CodigoTipo = 0;

            msgSucesso = "Registro adicionado com sucesso!";

            Response.Redirect("/Plataformas");
        }
    }

}
