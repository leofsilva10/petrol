using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Petrol.Pages.TiposPlataformas
{
    public class EditModel : PageModel
    {
        public Entities.TipoPlataforma infoTipoPlataforma = new Entities.TipoPlataforma();
        public List<Entities.TipoPlataforma> listaTiposPlataformas = new List<Entities.TipoPlataforma>();
        public string msgErro = "";
        public string msgSucesso = "";
        public int _codigo;
        public string _nome;
        public string _descricao;
        public string _lamina;
        public int _perfuracao;
        public int _producao;
        public int _controlePocos;
        public string _escoamento;
        public string _vantagem;
        public string _imagem;

        public void OnGet()
        {
            // Obter código do tipo de plataforma selecionada.
            string codigo = Request.Query["codigo"];

            var clsTipoPlataforma = new Entities.TipoPlataforma();

            // Listar plataforma a ter seus dados editados.
            listaTiposPlataformas = clsTipoPlataforma.ListarTiposPlataformas(int.Parse(codigo), "");

            foreach (var _plataforma in listaTiposPlataformas)
            {
                _codigo = _plataforma.Codigo;
                _nome = _plataforma.Nome;
                _descricao = _plataforma.Descricao;
                _lamina = _plataforma.Lamina;
                _perfuracao = _plataforma.Perfuracao;
                _producao = _plataforma.Producao;
                _controlePocos = _plataforma.ControlePocos;
                _escoamento = _plataforma.Escoamento;
                _vantagem = _plataforma.Vantagem;
                _imagem = _plataforma.Imagem;
                break;
            }
        }

        public void OnPost()
        {
            // Obter dados informados pelo usuário.

            infoTipoPlataforma.Codigo = int.Parse(Request.Form["codigo"]);
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

            if (infoTipoPlataforma.Nome.Length == 0)
            {
                msgErro = "Favor preencher todos os dados.";
                return;
            }

            // Editar o tipo de plataforma.

            try
            {
                var clsTipoPlataforma = new Entities.TipoPlataforma();

                if (clsTipoPlataforma.Editar(infoTipoPlataforma) == "0")
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

            msgSucesso = "Registro atualizado com sucesso!";

            Response.Redirect("/TiposPlataformas");
        }

    }
}
