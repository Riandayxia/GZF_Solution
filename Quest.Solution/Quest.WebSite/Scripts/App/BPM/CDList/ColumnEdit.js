/**-----------------------------------------------------------------
* @explanation:自定义列表纵列信息编辑
* @created：rainday
* @create time：2016-09-01 13:05
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表纵列信息编辑页面
*/
Ext.define("BPM.CDList.ColumnEdit", {
    extend: 'Ext.user.NEdit',
    xtype: 'bpm_cdlist_columnedit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    layout: {
        type: 'table',
        columns: 2
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { xtype: 'ndiction', fieldLabel: '数据索引', name: 'dataIndex', width: 300 },
        { xtype: 'textfield', fieldLabel: '列标头', name: 'header' },
        { xtype: 'numberfield', fieldLabel: '宽度', name: 'width' },
        { xtype: 'trigger', fieldLabel: '是否隐藏', name: 'hideable' },
        { xtype: 'trigger', fieldLabel: '是否排序', name: 'sortable', colspan: 2 },
        { xtype: 'textarea', fieldLabel: 'Config', name: 'Other', width: 600, colspan: 2 },
    ]
});
