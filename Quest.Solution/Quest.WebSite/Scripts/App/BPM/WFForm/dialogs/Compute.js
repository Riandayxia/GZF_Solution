/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Compute", {
    extend: 'Ext.user.NEdit',
    items: [{
        xtype: 'tabpanel',
        items: [{
            title: '属性',
            margin: '30 0 0 0',
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
                      { fieldLabel: '宽度', name: 'Width', xtype: 'textfield' },
                      { fieldLabel: '小数位数', name: 'Width', xtype: 'textfield' },
                  
                      { fieldLabel: '只读', name: 'Width', xtype: 'combobox', margin: '0 0 50 0' },

                      { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
                      { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                      { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
                ]
            }],
            layout: 'fit',
        }, {
            title: '表达式',
     
            margin: '250 0 0 0',
            items: [{

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