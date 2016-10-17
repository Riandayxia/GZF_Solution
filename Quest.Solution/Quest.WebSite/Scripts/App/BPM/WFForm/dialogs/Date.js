/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Date", {
    extend: 'Ext.user.NEdit',
    items: [{
        xtype: 'tabpanel',
        items: [{
            title: '属性',
            margin: '10 0 0 0',
            items: [{
                layout: {
                    type: 'table',
                    columns: 1
                },
                defaults: {
                    labelAlign: 'right',
                    width: 600
                },
                items: [
                     { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
                     { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
                     { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
                     { fieldLabel: '绑定字段', name: 'Width', xtype: 'combobox', width: 408 },
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
                     { fieldLabel: '宽度', name: 'Width', xtype: 'textfield', width: 408 },
                    {
                        fieldLabel: '选择范围', name: 'Height', xtype: 'fieldcontainer',
                        layout: 'hbox',
                        items: [
                            {
                                xtype: 'datefield', format: 'Y-m-d',
                                width: 135,
                            },
                            {
                                fieldLabel: '至',
                                labelWidth: 10,
                                width: 135,
                                xtype: 'datefield', format: 'Y-m-d',
                            },
                            {
                                boxLabel: '今天以前',
                                xtype: 'checkboxfield'
                            },
                            {
                                boxLabel: '今天以后',
                                xtype: 'checkboxfield'
                            },
                            {
                                boxLabel: '当前月',
                                xtype: 'checkboxfield'
                            }
                        ]
                    },
                     { fieldLabel: '时间', name: 'Width', xtype: 'checkboxfield', boxLabel: '允许选择时间' },
                     { fieldLabel: '格式', name: 'Width', xtype: 'textfield', value: '例：yyyy-MM-dd HH:mm', margin: '0 0 50 0', width: 408 },
                     { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
                     { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                     { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
                ]
            }],
            layout: 'fit',
        }, {
            title: '事件',
            margin: '10 0 0 0',
            items: [{
                layout: {
                    type: 'table',
                    columns: 1
                },
                defaults: {
                    labelAlign: 'right',
                    width: 450
                },
                items: [
                     { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
                     { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
                     { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
                     { fieldLabel: '事件', name: 'Width', xtype: 'combobox', width: 300 },
                     { fieldLabel: '事件脚本', name: 'Width', xtype: 'textarea', margin: '0 0 50 0', height: 150, value: 'function methodName(srcObj){}' },
                     { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
                     { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                     { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
                ]
            }],
            layout: 'fit',
        }]
    }]
});