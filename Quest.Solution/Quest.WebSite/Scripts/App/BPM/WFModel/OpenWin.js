//Layout 的文件路径
Ext.define("BPM.WFModel.OpenWin", {
    extend: 'Ext.panel.Panel',
    xtype: 'bpm_wfmodel_openwin',
    layout: 'border',
    //联系 Grid
    requires: ['BPM.WFModel.Grid'],
    items: [{
        //布局方位（东西南北中）
        region: 'center',
        //Grid 的xtype
        xtype: 'bpm_wfmodel_grid'
    }]
});
