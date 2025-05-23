using WebRestApi.Dto;

namespace WebRestApi.Repository;

public static class ProductMock
{
    public static IEnumerable<ProductDto> Products => new List<ProductDto>
    {
        new ProductDto(Guid.Parse("a1f1b790-0001-4c9d-b001-111111111111"), "Notebook Dell Inspiron 15", 3999.99m),
        new ProductDto(Guid.Parse("a1f1b790-0002-4c9d-b002-222222222222"), "Smartphone Samsung Galaxy S23", 4999.00m),
        new ProductDto(Guid.Parse("a1f1b790-0003-4c9d-b003-333333333333"), "Monitor LG Ultrawide 29\"", 1299.90m),
        new ProductDto(Guid.Parse("a1f1b790-0004-4c9d-b004-444444444444"), "Teclado Mecânico Redragon Kumara", 289.50m),
        new ProductDto(Guid.Parse("a1f1b790-0005-4c9d-b005-555555555555"), "Mouse Logitech MX Master 3S", 599.00m),
        new ProductDto(Guid.Parse("a1f1b790-0006-4c9d-b006-666666666666"), "Headset HyperX Cloud II", 499.99m),
        new ProductDto(Guid.Parse("a1f1b790-0007-4c9d-b007-777777777777"), "HD Externo Seagate 2TB", 399.00m),
        new ProductDto(Guid.Parse("a1f1b790-0008-4c9d-b008-888888888888"), "Webcam Logitech C920", 449.90m),
        new ProductDto(Guid.Parse("a1f1b790-0009-4c9d-b009-999999999999"), "Cadeira Gamer ThunderX3", 1299.00m),
        new ProductDto(Guid.Parse("a1f1b790-0010-4c9d-b010-aaaaaaaabaaa"), "Impressora HP DeskJet", 749.00m),
        new ProductDto(Guid.Parse("a1f1b790-0011-4c9d-b011-bbbbbbbbbbbb"), "Smartwatch Amazfit GTR 4", 1199.00m),
        new ProductDto(Guid.Parse("a1f1b790-0012-4c9d-b012-cccccccccccc"), "Tablet Samsung Galaxy Tab S8", 3799.00m),
        new ProductDto(Guid.Parse("a1f1b790-0013-4c9d-b013-dddddddddddd"), "Echo Dot 5ª Geração", 379.99m),
        new ProductDto(Guid.Parse("a1f1b790-0014-4c9d-b014-eeeeeeeeeeee"), "Placa de Vídeo RTX 4070", 4599.90m),
        new ProductDto(Guid.Parse("a1f1b790-0015-4c9d-b015-ffffffffffff"), "Processador AMD Ryzen 7 5800X", 1799.00m),
        new ProductDto(Guid.Parse("a1f1b790-0016-4c9d-b016-aaaa1111bbbb"), "Fonte Corsair 750W", 549.00m),
        new ProductDto(Guid.Parse("a1f1b790-0017-4c9d-b017-bbbb2222cccc"), "Memória RAM Corsair 16GB", 379.90m),
        new ProductDto(Guid.Parse("a1f1b790-0018-4c9d-b018-cccc3333dddd"), "SSD Kingston NVMe 1TB", 479.00m),
        new ProductDto(Guid.Parse("a1f1b790-0019-4c9d-b019-dddd4444eeee"), "Gabinete NZXT H510", 649.00m),
        new ProductDto(Guid.Parse("a1f1b790-0020-4c9d-b020-eeee5555ffff"), "Roteador TP-Link AX1800", 499.00m)
    };
}