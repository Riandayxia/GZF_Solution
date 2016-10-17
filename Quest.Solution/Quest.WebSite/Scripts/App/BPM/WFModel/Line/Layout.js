/*
* 步骤参数设置
*/
Ext.define("BPM.WFModel.Line.Layout", {
    extend: 'Ext.container.Container',
    xtype: 'line_layout',
    //layout: 'border',
    requires: [
         'BPM.WFModel.Line.Judge',
    ],
    items: [{
        xtype: 'line_judge',
        listeners: {
            formSubmit: function (but, win) {
                win.up('line_layout').SetJudge(but, win);
            }
        }
    }],
    // 初始化渲染
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.InitData();
    },
    // 初始化数据
    InitData: function (data) {
        var me = this;
        me.setConfig(data);

        me.line = {};
        me.wfData.Lines.forEach(function (item, index) {
            if (item.Id == me.lineId) {
                me.line = item;
            }
        });
        me.SetJudgeFrom(me.line, me.wfData.DBTableName);
    },
    // 设置判断
    SetJudge: function (but, win) {
        var me = this;
        var form = win.getForm();
        var data = form.getValues();
        //var filterWin = win.down('line_filter');
        //var fdata = filterWin.getData();
        //Ext.apply(data, {
        //    Expression: fdata,
        //    Field: '',
        //    G_Op: null,
        //    Op: NaN,
        //    Type: NaN,
        //    Value: NaN
        //});
        me.wfForm.addLine(data);
        win.up('window').hide();
    },
    // 设置判断数据
    SetJudgeFrom: function (data, tName) {
        var me = this;
        var wForm = me.down('line_judge');
        wForm.setData(data.Expression, tName);
        var form = wForm.getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(data);
    }
});