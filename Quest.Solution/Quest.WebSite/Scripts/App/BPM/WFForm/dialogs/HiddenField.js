/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.HiddenField", {
    extend: 'Ext.user.NEdit',
    xtype: 'hiddenfield_edit',
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
        width: 400,
        flex: 1
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
        { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
        { fieldLabel: '绑定字段', name: 'Width', xtype: 'combobox' },
   
        {
            fieldLabel: '默认值', name: 'Height', xtype: 'fieldcontainer',
            layout: 'hbox',
            items: [
                {
                    xtype: 'textfield',
                }, {
                    xtype: 'combobox',
                    width: 135,
                }
            ]
        },
        { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});