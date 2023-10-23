using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages.TiposPlataformas
{
    public class CreateModel : PageModel
    {
        public Entities.TipoPlataforma infoTipoPlataforma = new Entities.TipoPlataforma();
        public string msgErro = "";
        public string msgSucesso = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Obter dados informados pelo usuário.

            infoTipoPlataforma.Nome = Request.Form["nome"];
            infoTipoPlataforma.Descricao = Request.Form["descricao"];
            infoTipoPlataforma.Lamina = Request.Form["lamina"];
            infoTipoPlataforma.Perfuracao = Request.Form["perfuracao"] == "on" ? 1 : 0;
            infoTipoPlataforma.Producao = Request.Form["producao"] == "on" ? 1 : 0;
            infoTipoPlataforma.ControlePocos = Request.Form["controle_pocos"] == "on" ? 1 : 0;
            infoTipoPlataforma.Escoamento = Request.Form["escoamento"];
            infoTipoPlataforma.Vantagem = Request.Form["vantagem"];
            infoTipoPlataforma.Imagem = Request.Form["imagem"];

            // Verificar se os dados foram cadastrados corretamente.

            if (infoTipoPlataforma.Nome.Length == 0 || infoTipoPlataforma.Descricao.Length == 0)
            {
                msgErro = "Favor preencher todos os dados.";
                return;
            }

            // Adicionar o novo tipo plataforma.

            try
            {
                var classTipoPlataforma = new Entities.TipoPlataforma();

                if (classTipoPlataforma.Adicionar(infoTipoPlataforma) == "0")
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

            msgSucesso = "Registro adicionado com sucesso!";

            Response.Redirect("/TiposPlataformas");
        }

    }
}
