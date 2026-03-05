using BackendTest.Application.Features.Purchases;
using BackendTest.Application.Features.Purchases.DownloadPurchaseReport;
using BackendTest.Exceptions;
using BackendTest.Model;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class DownloadPurchaseReportHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingReport_ReturnsDownloadResponse()
    {
        // Arrange
        var reportRepositoryMock = new Mock<IReportRepository>();
        var reportId = Guid.CreateVersion7();
        var content = new byte[] { 1, 2, 3 };
        var report = new Report(reportId, 123, Report.ReportFormat.Csv, content, "text/csv", "csv");

        reportRepositoryMock
            .Setup(repository => repository.GetById(reportId, 123, It.IsAny<CancellationToken>()))
            .ReturnsAsync(report);

        var handler = new DownloadPurchaseReportHandler(reportRepositoryMock.Object);

        // Act
        var response = await handler.Handle(new DownloadPurchaseReportRequest(123, reportId), CancellationToken.None);

        // Assert
        response.Content.Should().BeEquivalentTo(content);
        response.ContentType.Should().Be("text/csv");
        response.FileName.Should().Be($"purchase_report_{reportId}.csv");
    }

    [Fact]
    public async Task Handle_WithMissingReport_ThrowsNotFoundException()
    {
        // Arrange
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock
            .Setup(repository => repository.GetById(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Report)null);

        var handler = new DownloadPurchaseReportHandler(reportRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DownloadPurchaseReportRequest(321, Guid.CreateVersion7()), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
