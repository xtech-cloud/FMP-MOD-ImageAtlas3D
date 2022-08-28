
using Microsoft.AspNetCore.Components;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Proto;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Bridge;
using XTC.FMP.MOD.ImageAtlas3D.LIB.MVCS;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Razor
{
    public partial class DesignerComponent
    {
        public class DesignerUiBridge : IDesignerUiBridge
        {

            public DesignerUiBridge(DesignerComponent _razor)
            {
                razor_ = _razor;
            }

            public void Alert(string _code, string _message, object? _context)
            {
                if (null == razor_.messageService_)
                    return;
                Task.Run(async () =>
                {
                    await razor_.messageService_.Error(_message);
                }); 
            }


            public void RefreshReadStyleSheet(IDTO _dto, object? _context)
            {
                var dto = _dto as DesignerReadStylesResponseDTO;
                razor_.__debugReadStyleSheet = dto?.Value.ToString();
            }

            public void RefreshWriteStyle(IDTO _dto, object? _context)
            {
                var dto = _dto as BlankResponseDTO;
                razor_.__debugWriteStyle = dto?.Value.ToString();
            }

            public void RefreshReadInstances(IDTO _dto, object? _context)
            {
                var dto = _dto as DesignerReadInstancesResponseDTO;
                razor_.__debugReadInstances = dto?.Value.ToString();
            }

            public void RefreshWriteInstances(IDTO _dto, object? _context)
            {
                var dto = _dto as BlankResponseDTO;
                razor_.__debugWriteInstances = dto?.Value.ToString();
            }


            private DesignerComponent razor_;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task __debugClick()
        {
            var bridge = (getFacade()?.getViewBridge() as IDesignerViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }

            var reqReadStyleSheet = new ScopeRequest();
            var dtoReadStyleSheet = new ScopeRequestDTO(reqReadStyleSheet);
            logger_?.Trace("invoke OnReadStyleSheetSubmit");
            await bridge.OnReadStyleSheetSubmit(dtoReadStyleSheet, null);

            var reqWriteStyle = new DesignerWriteStylesRequest();
            var dtoWriteStyle = new DesignerWriteStylesRequestDTO(reqWriteStyle);
            logger_?.Trace("invoke OnWriteStyleSubmit");
            await bridge.OnWriteStyleSubmit(dtoWriteStyle, null);

            var reqReadInstances = new ScopeRequest();
            var dtoReadInstances = new ScopeRequestDTO(reqReadInstances);
            logger_?.Trace("invoke OnReadInstancesSubmit");
            await bridge.OnReadInstancesSubmit(dtoReadInstances, null);

            var reqWriteInstances = new DesignerWriteInstancesRequest();
            var dtoWriteInstances = new DesignerWriteInstancesRequestDTO(reqWriteInstances);
            logger_?.Trace("invoke OnWriteInstancesSubmit");
            await bridge.OnWriteInstancesSubmit(dtoWriteInstances, null);

        }


        private string? __debugReadStyleSheet;

        private string? __debugWriteStyle;

        private string? __debugReadInstances;

        private string? __debugWriteInstances;

    }
}
