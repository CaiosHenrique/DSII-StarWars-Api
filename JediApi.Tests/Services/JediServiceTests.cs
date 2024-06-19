using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        

        //poderia ter feito uma api de league </3


        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {       
            var IdJedi = 1;
            var jedi = new Jedi { 
                Id = IdJedi, 
                Name = "R2D2 nao entendo de starwars" 
            };
            _repositoryMock.Setup(mock => mock.GetByIdAsync(IdJedi)).ReturnsAsync(jedi);

            var novojedi = await _service.GetByIdAsync(IdJedi);
            Assert.NotNull(novojedi);

            Assert.Equal(jedi.Id, novojedi.Id);
            Assert.Equal(jedi.Name, novojedi.Name);
        }

        [Fact]
        public async Task GetById_NotFound()
        {            
            var IdJedi = 1;
            _repositoryMock.Setup(mock => mock.GetByIdAsync(IdJedi)).ReturnsAsync((Jedi)null);
            var result = await _service.GetByIdAsync(IdJedi);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            
            var listadosjedi = new List<Jedi>
            {
                new Jedi { 
                    Id = 1,
                    Name = "aquele carinha dourado amigo do R2D2" 
                },
                new Jedi { 
                    Id = 2,
                    Name = "darth vader" 
                }
            };
            _repositoryMock.Setup(mock => mock.GetAllAsync()).ReturnsAsync(listadosjedi);
            var jedis = await _service.GetAllAsync();

            Assert.NotNull(jedis);

            Assert.Equal(listadosjedi.Count, jedis.Count);
            Assert.Equal(listadosjedi[0].Id, jedis[0].Id);
            Assert.Equal(listadosjedi[1].Id, jedis[1].Id);
        }
    }
}
