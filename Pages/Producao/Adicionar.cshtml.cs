using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages.Producao
{
    public class CreateModel : PageModel
    {
        public Entities.Producao infoProducao = new Entities.Producao();
        public List<Entities.Plataforma> listaPlataformas = new List<Entities.Plataforma>();
        public string msgErro = "";
        public string msgSucesso = "";
        public void OnGet()
        {
            var clsPlataforma = new Entities.Plataforma();

            //Preencher a lista de tipos de plataformas.
            listaPlataformas = clsPlataforma.ListarPlataformas(0, 0, "");
        }

        public void OnPost()
        {
            // Obter dados informados pelo usuário.

            infoProducao.Valor = int.Parse(Request.Form["valor"]);
            infoProducao.CodigoPlataforma = int.Parse(Request.Form["codigoPlataforma"]);
            infoProducao.DataRegistro = Request.Form["data_registro"];

            // Verificar se os dados foram cadastrados corretamente.

            if (infoProducao.Valor == 0 || infoProducao.CodigoPlataforma == 0)
            {
                msgErro = "Favor preencher todos os dados.";
                return;
            }

            // Adicionar nova produção.

            try
            {
                var clsProducao = new Entities.Producao();

                if (clsProducao.Adicionar(infoProducao) == "0")
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

            infoProducao.Valor = 0;
            infoProducao.CodigoPlataforma = 0;

            msgSucesso = "Registro adicionado com sucesso!";

            Response.Redirect("/Producao");
        }

    }
}
