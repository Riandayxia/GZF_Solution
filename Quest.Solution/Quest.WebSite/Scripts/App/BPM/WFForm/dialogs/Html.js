/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Html", {
    extend: 'Ext.user.NEdit',
    xtype: 'html_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 1
    },
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
        { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
        { fieldLabel: '绑定字段', name: 'Title', xtype: 'combobox' },
        { fieldLabel: '宽度', name: 'Width', xtype: 'textfield'},
        { fieldLabel: '高度', name: 'Height', xtype: 'textfield' },
        { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});