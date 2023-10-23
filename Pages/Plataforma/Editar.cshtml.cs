using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages.Plataformas
{
    public class EditModel : PageModel
    {
        public Entities.Plataforma infoPlataforma = new Entities.Plataforma();
        public List<Entities.Plataforma> listaPlataformas = new List<Entities.Plataforma>();
        public List<Entities.TipoPlataforma> listaTiposPlataformas = new List<Entities.TipoPlataforma>();
        public string msgErro = "";
        public string msgSucesso = "";
        public int _codigo;
        public string _nome;
        public int _codigoTipo;

        public void OnGet()
        {
            // Obter código da plataforma selecionada.
            string codigo = Request.Query["codigo"];

            var classPlataforma = new Entities.Plataforma();

            // Listar plataforma a ter seus dados editados.
            listaPlataformas = classPlataforma.ListarPlataformas(int.Parse(codigo), 0, "");

            foreach (var _plataforma in listaPlataformas)
            {
                _codigo = _plataforma.Codigo;
                _nome = _plataforma.Nome;
                _codigoTipo = _plataforma.CodigoTipo;
                break;
            }

            var classTipoPlataforma = new Entities.TipoPlataforma();

            // Listar os tipos de plataformas disponíveis.
            listaTiposPlataformas = classTipoPlataforma.ListarTiposPlataformas(0, "");
        }

        public void OnPost()
        {
            // Obter dados informados pelo usuário.

            infoPlataforma.Codigo = int.Parse(Request.Form["codigo"]);
            infoPlataforma.Nome = Request.Form["nome"];
            infoPlataforma.CodigoTipo = int.Parse(Request.Form["tipo"]);

            // Verificar se os dados foram cadastrados corretamente.

            if (infoPlataforma.Nome.Length == 0 || infoPlataforma.CodigoTipo == 0)
            {
                msgErro = "Favor preencher todos os dados.";
                return;
            }

            // Editar a plataforma.

            try
            {
                var classPlataforma = new Entities.Plataforma();

                if (classPlataforma.Editar(infoPlataforma) == "0")
                {
                    msgErro = "Erro ao atualizar o registro.";
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

            msgSucesso = "Registro atualizado com sucesso!";

            Response.Redirect("/Plataformas");
        }
    }
}
