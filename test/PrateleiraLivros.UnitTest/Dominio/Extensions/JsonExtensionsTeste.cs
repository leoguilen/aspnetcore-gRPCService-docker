using PrateleiraLivros.Dominio.Extensions;
using Xunit;

namespace PrateleiraLivros.UnitTest.Dominio.Extensions
{
    [Collection("Extensions")]
    public class JsonExtensionsTeste
    {
        [Fact]
        public void SerializarObjetoCorretamente()
        {
            var classeTeste = new ClasseTeste 
            { 
                Value = "testando serializacao" 
            };

            var objetoSerializado = classeTeste.ToJson();

            Assert.Equal("{\"Value\":\"testando serializacao\"}", objetoSerializado);
        }

        [Fact]
        public void DeserializarObjetoCorretamente()
        {
            var json = "{\"Value\":\"testando serializacao\"}";

            var objetoDeserializado = json.FromJson<ClasseTeste>();

            Assert.IsType<ClasseTeste>(objetoDeserializado);
            Assert.Equal("testando serializacao", objetoDeserializado.Value);
        }
    }

    public class ClasseTeste
    {
        public string Value { get; set; }
    }
}
