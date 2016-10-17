/*
* 步骤参数设置
*/
Ext.define("BPM.WFModel.Step.Layout", {
    extend: 'Ext.container.Container',
    xtype: 'step_layout',
    //layout: 'border',
    requires: [
         'BPM.WFModel.Step.BaseAttr',
    ],
    items: [{
        xtype: 'step_baseAttr',
        listeners: {
            formSubmit: function (but, win) {
                win.up('step_layout').SetBaseAttr(but, win);
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

        me.step = {};
        me.steps = [];
        me.wfData.Steps.forEach(function (item, index) {
            if (item.Id == me.stepId) {
                me.step = item;
            } else {
                me.steps.push(item);
            }
        });
        me.SetBaseAttrForm(me.step);
        //me.SetBehaviorForm(me.step);
    },
    // 设置基本信息
    SetBaseAttr: function (but, win) {
        var me = this;
        var form = win.getForm();
        var data = form.getValues();
        Ext.apply(me.step, data);
        me.wfForm.addStepData(me.step);
        me.wfForm.setStepText(data.Id, data.Name);
        win.up('window').hide();
    },
    // 设置策略信息
    SetBehavior: function (but, win) {
        var me = this;
        var form = win.getForm();
        var data = form.getValues();
        Ext.apply(me.step.Behavior, data);
        me.wfForm.addStepData(me.step);
        me.wfForm.setStepText(data.Id, data.Name);
        win.up('window').hide();
    },
    // 设置基本信息表单数据
    SetBaseAttrForm: function (data) {
        var me = this;
        var wForm = me.down('step_baseAttr');
        var form = wForm.getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(data);
    },
    // 设置策略信息表单数据
    SetBehaviorForm: function (data) {
        var me = this;
        var wForm = me.down('step_behavior');
        var form = wForm.getForm();
        form.reset();
        if (!data.Behavior) data.Behavior = {};
        //给添加窗体赋值
        form.setValues(data.Behavior);
    },

});