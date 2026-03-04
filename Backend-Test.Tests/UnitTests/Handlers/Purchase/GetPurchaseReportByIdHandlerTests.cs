using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Application.Reporting;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Model;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class GetPurchaseReportByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingReport_ReturnsExistingReportWithDownloadUrl()
    {
        // Arrange
        var reportDataSourceMock = new Mock<IPurchaseReportDataSource>();
        var reportRepositoryMock = new Mock<IReportRepository>();
        var reportDownloadLinkProviderMock = new Mock<IReportDownloadLinkProvider>();
        var reportGeneratorFactoryMock = new Mock<IPurchaseReportGeneratorFactory>();

        var reportId = Guid.CreateVersion7();
        var existingReport = new Report(reportId, 777, Report.ReportFormat.Csv, new byte[] { 1 }, "text/csv", "csv");

        reportRepositoryMock
            .Setup(repository => repository.GetByPurchaseId(777, Report.ReportFormat.Csv, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingReport);

        reportDownloadLinkProviderMock
            .Setup(provider => provider.CreateDownloadUrl(777, reportId, It.IsAny<CancellationToken>()))
            .ReturnsAsync($"/purchases/777/reports/{reportId}");

        var handler = new GetPurchaseReportByIdHandler(
            reportDataSourceMock.Object,
            reportRepositoryMock.Object,
            reportDownloadLinkProviderMock.Object,
            reportGeneratorFactoryMock.Object);

        // Act
        var response = await handler.Handle(new GetPurchaseReportByIdRequest(777), CancellationToken.None);

        // Assert
        response.PurchaseId.Should().Be(777);
        response.ReportId.Should().Be(reportId);
        response.DownloadUrl.Should().Be($"/purchases/777/reports/{reportId}");

        reportDataSourceMock.Verify(dataSource => dataSource.GetData(It.IsAny<GetPurchaseReportByIdRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        reportGeneratorFactoryMock.Verify(factory => factory.Create(It.IsAny<Report.ReportFormat>()), Times.Never);
        reportRepositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Report>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithoutExistingReport_GeneratesStoresAndReturnsDownloadUrl()
    {
        // Arrange
        var reportDataSourceMock = new Mock<IPurchaseReportDataSource>();
        var reportRepositoryMock = new Mock<IReportRepository>();
        var reportDownloadLinkProviderMock = new Mock<IReportDownloadLinkProvider>();
        var reportGeneratorFactoryMock = new Mock<IPurchaseReportGeneratorFactory>();
        var reportGeneratorMock = new Mock<IPurchaseReportGenerator>();

        var customer = new BackendTest.Model.Person(2000, "Report", "Customer", 1990);
        var item = new PurchaseProductItem(3000, 2);
        var product = new BackendTest.Model.Product(3000, "Product", "Type", 11m);
        var reportData = new PurchaseReportData(888, customer, [item], new Dictionary<int, BackendTest.Model.Product> { [3000] = product });
        var generatedReportId = Guid.CreateVersion7();
        var generatedReport = new Report(generatedReportId, 888, Report.ReportFormat.Csv, new byte[] { 4, 5, 6 }, "text/csv", "csv");

        reportRepositoryMock
            .Setup(repository => repository.GetByPurchaseId(888, Report.ReportFormat.Csv, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Report)null);
        reportDataSourceMock
            .Setup(dataSource => dataSource.GetData(It.Is<GetPurchaseReportByIdRequest>(request => request.PurchaseId == 888), It.IsAny<CancellationToken>()))
            .ReturnsAsync(reportData);
        reportGeneratorFactoryMock
            .Setup(factory => factory.Create(Report.ReportFormat.Csv))
            .Returns(reportGeneratorMock.Object);
        reportGeneratorMock
            .Setup(generator => generator.Generate(reportData))
            .Returns(generatedReport);
        reportRepositoryMock
            .Setup(repository => repository.TryAdd(generatedReport, It.IsAny<CancellationToken>()))
            .ReturnsAsync(generatedReport);
        reportDownloadLinkProviderMock
            .Setup(provider => provider.CreateDownloadUrl(888, generatedReportId, It.IsAny<CancellationToken>()))
            .ReturnsAsync($"/purchases/888/reports/{generatedReportId}");

        var handler = new GetPurchaseReportByIdHandler(
            reportDataSourceMock.Object,
            reportRepositoryMock.Object,
            reportDownloadLinkProviderMock.Object,
            reportGeneratorFactoryMock.Object);

        // Act
        var response = await handler.Handle(new GetPurchaseReportByIdRequest(888), CancellationToken.None);

        // Assert
        response.PurchaseId.Should().Be(888);
        response.ReportId.Should().Be(generatedReportId);
        response.DownloadUrl.Should().Be($"/purchases/888/reports/{generatedReportId}");

        reportDataSourceMock.Verify(dataSource => dataSource.GetData(It.IsAny<GetPurchaseReportByIdRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        reportGeneratorFactoryMock.Verify(factory => factory.Create(Report.ReportFormat.Csv), Times.Once);
        reportGeneratorMock.Verify(generator => generator.Generate(reportData), Times.Once);
        reportRepositoryMock.Verify(repository => repository.TryAdd(generatedReport, It.IsAny<CancellationToken>()), Times.Once);
    }
}
