/*
* 配置基本参数
*/
Ext.define("BPM.WFModel.Step.BaseAttr", {
    extend: 'Ext.user.NEdit',
    xtype: 'step_baseAttr',
    saveBtnText: '确定',
    formSubmit: false,
    modal: true,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 2
    },
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    //requires:["UX.User.SelectCon"],
    items: [
        { fieldLabel: '步骤ID', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: 'x', name: 'Position_x', xtype: 'hiddenfield' },
        { fieldLabel: 'y', name: 'Position_y', xtype: 'hiddenfield', value: 'normal' },
        { fieldLabel: '步骤名称', name: 'Name', xtype: 'textfield', colspan: 2, width: 600 },
        { fieldLabel: '工时(小时)', name: 'WorkTime', xtype: 'textfield' },
         {
             xtype: 'combo', fieldLabel: '超期提示', name: 'ExpiredPrompt',
             displayField: 'text', valueField: 'value',
             store: Ext.create('Ext.data.Store', {
                 fields: ['text', 'value'],
                 data: [
                     { value: 1, text: "提示" },
                     { value: 0, text: "不提示" }
                 ]
             })
         },

        {
            xtype: 'textfield', fieldLabel: '处理者', name: 'DefaultUser', value: '00000000-0000-0000-0000-000000000001'
        },
        {
            xtype: 'textfield', fieldLabel: '表单Id', name: 'FormId', value: '00000000-0000-0000-0000-000000000001'
        },
       //{
       //    xtype: 'selectcon', fieldLabel: '表单', name: 'FormUrl', Id: 'FormId', edit: 'step_baseAttr', viewWin: 'BPM.WFForm.Grid', winTitle: '表单管理', winWidth: 700,
       //    winHeight: 500, bindingName: 'Name', bindingId: 'Id', more: false,
       // },
        { fieldLabel: '说明', name: 'Note', xtype: 'textarea', colspan: 2, width: 600 }
    ],
    //afterRender: function () {
    //    var me = this;
    //    me.callParent(arguments);
    //    var workTime = me.down('[name="WorkTime"]')
    //    var store = Ext.create('Ext.data.Store', {
    //        fields: ['text', 'value'],
    //        data: [
    //            { value: 1, text: "提示" },
    //            { value: 0, text: "不提示" }
    //        ]
    //    });
    //    workTime.bingStore(store);
    //}
});