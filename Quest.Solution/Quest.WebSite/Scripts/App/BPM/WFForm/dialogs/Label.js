/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Label", {
    extend: 'Ext.user.NEdit',
    xtype: 'label_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 2
    },
    defaults: {
        labelAlign: 'right',
        width: 300,
        flex: 1
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
        { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
        { fieldLabel: '默认值', name: 'Title', xtype: 'textfield' },
        { fieldLabel: '', name: 'Width', xtype: 'combobox', width: 100 },
        { fieldLabel: '字号', name: 'Height', xtype: 'textfield', colspan: 2 },
        
    
        { fieldLabel: '高度', name: 'Height', xtype: 'textfield', colspan: 2 },
      
        {
            fieldLabel: '样式', name: 'Height', xtype: 'fieldcontainer',
            defaultType: 'checkboxfield',
            layout: 'hbox',
            items: [
                {
                    boxLabel: '粗体',
                    name: 'checkboxfield',
                    inputValue: 'm',
                }, {
                    boxLabel: '斜体',
                    name: 'checkboxfield',
                    inputValue: 'l',
                }
            ]
        },
        { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});