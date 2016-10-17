/**-----------------------------------------------------------------
* @explanation:流程运行页面
* @created：rainday
* @create time：2016-08-09 10:11
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 流程运行页面
*/
Ext.define("BPM.WFModel.Run", {
    extend: 'Ext.panel.Panel',
    xtype: 'bpm_wfmodel_run',
    layout: 'fit',
    cls: 'ueFrom',
    style: 'border: 0px !important;',
    // 初始化
    initComponent: function () {
        var me = this;
        me.dockedItems = [{
            xtype: 'toolbar',
            dock: 'top',
            cls:'showFrom',
            items: [
                {
                    text: '流程图', iconCls: 'wficon_flow', handler: function (but) {
                        me.openFlow();
                    }
                },
                {
                    text: '打印', iconCls: 'wficon_printer', handler: function (but) {
                        me.printerForm();
                    }
                },
                {
                    text: '保存', iconCls: 'wficon_saveFlow', handler: function (but) {
                        me.saveForm();
                    }
                },
                {
                    text: '刷新', iconCls: 'wficon_refresh', handler: function (but) {
                        me.refreshForm();
                    }
                },
                {
                    text: '发送', iconCls: 'wficon_arrow_medium_right', handler: function (but) {
                        me.sendFlow();
                    }
                }
            ]
        }]
        var params = Ext.decode(me.Menu.Params);
        util.request({
            url: 'WFRunInstance/GetFirstStep',
            params: { flowId: params.flowid },
            async: false,
            success: function (result) {
                if (result.success) {
                    me.FormData = result.data;
                    me.html = '<form  id="' + result.data.DBTable + '_form" >' + result.data.DesignHtml + "</form>";
                }
            }
        });
        this.callParent(arguments);
    },
    // 打开流程图
    openFlow: function () {
        var me = this;
        var params = Ext.decode(me.Menu.Params);

        if (!me.flowlWin) {
            ////获得 添加窗体对象
            var object = {
                winTitle: '流程图', winWidth: '800px',winHeight:'60%', win: 'BPM.WFModel.Display', config: {
                    folwId: params.flowid, layout: 'fit',
                }
            }
            me.flowlWin = util.createWindow(object);
        }
        // 打开窗体
        me.flowlWin.show();
    },
    // 打印表单
    printerForm: function () {
        var me = this;
        alert('打印表单');
    },
    // 保存表单
    saveForm: function () {
        var me = this;
        util.request({
            url: me.FormData.Url_AddOrUpdate,
            form: me.FormData.DBTable + '_form',
            method: 'POST',
            async: false,
            success: function (result) {
                var form = document.getElementById(me.FormData.DBTable + '_form');
                form.reset()
                util.msgTip(result.msg);
            }
        });
    },
    // 刷新表单
    refreshForm: function () {
        var me = this;
        var form = document.getElementById(me.FormData.DBTable + '_form');
        form.reset()
    },
    // 发送表单
    sendFlow: function () {
        var me = this;
        var params = Ext.decode(me.Menu.Params);
        util.request({
            url: me.FormData.Url_AddOrUpdate,
            form: me.FormData.DBTable + '_form',
            params: { flowId: params.flowid },//参数
            method: 'POST',
            async: false,
            success: function (result) {
                var form = document.getElementById(me.FormData.DBTable + '_form');
                form.reset()
                util.msgTip(result.msg);
            }
        });
    }
});
