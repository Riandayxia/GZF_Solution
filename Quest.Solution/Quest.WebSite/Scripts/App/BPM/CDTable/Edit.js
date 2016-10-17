/*
* 数据表编辑页面
*/
Ext.define("BPM.CDTable.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'bpm_cdtable_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    layout: {
        type: 'table',
        columns: 1
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '数据库表名', name: 'Name', xtype: 'textfield', allowBlank: false },
        { fieldLabel: '别名', name: 'ByName', xtype: 'textfield', allowBlank: false },
        { fieldLabel: '创建者', name: 'Creator', xtype: 'textfield', allowBlank: false },
        { fieldLabel: '序号', name: 'Sort', xtype: 'numberfield', allowBlank: false, value: 1 },
        { fieldLabel: '描述', name: 'Desc', xtype: 'textarea', colspan: 2, width: 600 },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield', },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});