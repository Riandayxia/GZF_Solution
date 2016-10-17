/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Children", {
    extend: 'Ext.user.NEdit',
    requires: [
      'BPM.WFForm.dialogs.ChildrenGrid'
    ],
    xtype: 'children_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 3
    },
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
        { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
        { fieldLabel: '从表', name: 'Title', xtype: 'combobox' },
        { fieldLabel: '从表主键', name: 'DataConnection', xtype: 'combobox' },
        { fieldLabel: '主表字段', name: 'DataSheet', xtype: 'combobox' },
        { fieldLabel: '与主表关联字段', name: 'PrimaryKey', xtype: 'combobox' },
        {
            fieldLabel: '编辑方式',
            xtype: 'fieldcontainer',
            defaultType: 'radiofield',
            layout: 'hbox',
            items: [
                {
                    boxLabel: '常规',
                }, {
                    boxLabel: '弹出'
                }
            ]
        },
        { fieldLabel: '窗口宽度', name: 'PrimaryKey', xtype: 'textfield' },
        {
            fieldLabel: '编辑表单', name: 'Height', xtype: 'fieldcontainer',
            layout: 'hbox', colspan: 2,
            defaults: {
                flex: 1
            },
            items: [
                {
                    xtype: 'combobox',
                    width: 135,
                }, {
                    xtype: 'combobox',
                    margin: '0 0 0 10'
                }
            ]
        },
        { fieldLabel: '窗口高度', name: 'PrimaryKey', xtype: 'textfield' },
        { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
        {
            xtype: 'children_grid', colspan: 3, width: 950, height: 300, padding: '10,0,10,0'//'10 0 10 95',
        }
    ]
});