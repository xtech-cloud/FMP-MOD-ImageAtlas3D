
using XTC.FMP.MOD.ImageAtlas3D.LIB.Proto;

public class DesignerTest : DesignerUnitTestBase
{
    public DesignerTest(TestFixture _testFixture)
        : base(_testFixture)
    {
    }


    public override async Task ReadStyleSheetTest()
    {
        var request = new ScopeRequest();
        var response = await fixture_.getServiceDesigner().ReadStyleSheet(request, fixture_.context);
        Assert.Equal(0, response.Status.Code);
    }

    public override async Task WriteStyleTest()
    {
        var request = new DesignerWriteStylesRequest();
        var response = await fixture_.getServiceDesigner().WriteStyle(request, fixture_.context);
        Assert.Equal(0, response.Status.Code);
    }

    public override async Task ReadInstancesTest()
    {
        var request = new ScopeRequest();
        var response = await fixture_.getServiceDesigner().ReadInstances(request, fixture_.context);
        Assert.Equal(0, response.Status.Code);
    }

    public override async Task WriteInstancesTest()
    {
        var request = new DesignerWriteInstancesRequest();
        var response = await fixture_.getServiceDesigner().WriteInstances(request, fixture_.context);
        Assert.Equal(0, response.Status.Code);
    }

}
