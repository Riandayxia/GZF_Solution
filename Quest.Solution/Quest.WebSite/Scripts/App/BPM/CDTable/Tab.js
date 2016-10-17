Ext.define('BPM.CDTable.Tab', {
    extend: 'Ext.tab.Panel',
    xtype: 'bpm_cdtable_tab',
    stateId: 'bpm_cdtable_tab',
    requires: [
       'BPM.CDColumn.Grid',
       'BPM.CDController.Grid'
    ],
    items: [{
        title: '数据列',
        authHeight: true,
        layout: 'fit',
        xtype: "bpm_cdcolumn_grid"
    }, {
        title: '控制器',
        authHeight: true,
        layout: 'fit',
        xtype: "bpm_cdcontroller_grid"
    }]
});