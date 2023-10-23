using System.Data;
using System.Data.OracleClient;

namespace Petrol.Entities
{
    public class Producao
    {
        public int Codigo { get; set; }
        public int CodigoPlataforma { get; set; }
        public int Valor { get; set; }
        public string DataRegistro { get; set; }
        public List<Producao> ListarProducao(int codigoProducao, int codigoPlataforma)
        {
            // Definindo a lista de Produção (com base na classe).
            List<Producao> listaProducao = new List<Producao>();

            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure a ser executada para obter a lista de produção.
            OracleCommand cmd = new OracleCommand("PKG_PRODUCAO.SP_LISTA_PRODUCAO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada na stored procedure.

            OracleParameter param1_in = new OracleParameter("P_PROD_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = codigoProducao;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_PLAT_CD_CODIGO", OracleType.Int32);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = codigoPlataforma;
            cmd.Parameters.Add(param2_in);

            // Definindo o parâmetro de saída (cursor) da stored procedure.
            OracleParameter output = cmd.Parameters.Add("Cursor_Producao", OracleType.Cursor);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();

            // Lendo os dados retornados e adicionando-os à lista de Produção.
            while (reader.Read())
            {
                Producao producao = new Producao();
                producao.Codigo = reader.GetInt32(reader.GetOrdinal("PROD_CD_CODIGO"));
                producao.CodigoPlataforma = reader.GetInt32(reader.GetOrdinal("PLAT_CD_CODIGO"));
                producao.Valor = reader.GetInt32(reader.GetOrdinal("PROD_VR_PRODUCAO"));
                producao.DataRegistro = reader.GetString(reader.GetOrdinal("PROD_DT_REGISTRO"));

                listaProducao.Add(producao);
            }

            conn.Close();

            // Retornando a lista de Produção.
            return listaProducao;
        }

        public string Adicionar(Producao infoProducao)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para adicionar o registro.
            OracleCommand cmd = new OracleCommand("PKG_PRODUCAO.SP_ADICIONAR_PRODUCAO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada da stored procedure.

            OracleParameter param1_in = new OracleParameter("P_PLAT_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = infoProducao.CodigoPlataforma;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_PROD_VR_PRODUCAO", OracleType.Int32);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = infoProducao.Valor;
            cmd.Parameters.Add(param2_in);

            OracleParameter param3_in = new OracleParameter("P_PROD_DT_REGISTRO", OracleType.DateTime);
            param3_in.Direction = ParameterDirection.Input;
            param3_in.Value = infoProducao.DataRegistro;
            cmd.Parameters.Add(param3_in);

            // Definindo o parâmetro de saída da stored procedure.
            OracleParameter output = cmd.Parameters.Add("P_RESULT", OracleType.Int32);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            conn.Close();

            // Retornando o indicativo se a operação foi executada com sucesso ou não.
            return output.ToString();
        }

        public string Editar(Producao infoProducao)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para editar o registro.
            OracleCommand cmd = new OracleCommand("PKG_PRODUCAO.SP_EDITAR_PRODUCAO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada da stored procedure.

            OracleParameter param0_in = new OracleParameter("P_PROD_CD_CODIGO", OracleType.Int32);
            param0_in.Direction = ParameterDirection.Input;
            param0_in.Value = infoProducao.Codigo;
            cmd.Parameters.Add(param0_in);

            OracleParameter param1_in = new OracleParameter("P_PLAT_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = infoProducao.CodigoPlataforma;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_PROD_VR_PRODUCAO", OracleType.Int32);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = infoProducao.Valor;
            cmd.Parameters.Add(param2_in);

            OracleParameter param3_in = new OracleParameter("P_PROD_DT_REGISTRO", OracleType.DateTime);
            param3_in.Direction = ParameterDirection.Input;
            param3_in.Value = infoProducao.DataRegistro;
            cmd.Parameters.Add(param3_in);

            // Definindo o parâmetro de saída da stored procedure.
            OracleParameter output = cmd.Parameters.Add("P_RESULT", OracleType.Int32);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            conn.Close();

            // Retornando o indicativo se a operação foi executada com sucesso ou não.
            return output.ToString();
        }

        public void Excluir(int codigo)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para excluir o registro.
            OracleCommand cmd = new OracleCommand("PKG_PRODUCAO.SP_EXCLUIR_PRODUCAO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo o parâmetro de entrada da stored procedure.
            OracleParameter param1_in = new OracleParameter("P_PROD_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = codigo;
            cmd.Parameters.Add(param1_in);

            // Executando o procedimento de exclusão do registro.
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
