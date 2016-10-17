/*
* 筛选
*/
Ext.define("BPM.WFModel.Line.Judge", {
    extend: 'Ext.user.NEdit',
    xtype: 'line_judge',
    saveBtnText: '确定',
    requires: [
         'BPM.WFModel.Line.Filter',
    ],
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
    items: [
        { fieldLabel: '连线ID', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '起始步骤', name: 'FromID', xtype: 'hiddenfield' },
        { fieldLabel: '指向步骤', name: 'ToID', xtype: 'hiddenfield' },
        { fieldLabel: '连线名称', name: 'Text', xtype: 'textfield', colspan: 2 },
        { fieldLabel: 'Sql语句', name: 'TSql', xtype: 'textarea', colspan: 2, width: 600 },
        //{ fieldText: '表达式', name: 'Expression', xtype: 'line_filter', textWidth: 105, colspan: 2, width: 600 },
        { fieldLabel: '说明', name: 'Note', xtype: 'textarea', colspan: 2, width: 600 }
    ],
    setData: function (data, tName) {
        var me = this;
        //var wfilter = me.down('line_filter');
        //wfilter.DbTableName = tName;
        //wfilter.setData(data);
    }
});