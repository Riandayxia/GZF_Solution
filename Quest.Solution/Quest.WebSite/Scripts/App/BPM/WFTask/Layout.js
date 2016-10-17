/**-----------------------------------------------------------------
* @explanation:任务信息布局
* @created：rainday
* @create time：2016-08-19 09:35
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.WFTask.Layout', {
    extend: 'BPM.WFTask.Grid',
    xtype: 'bpm_wftask_layout',
    fullscreen: true,
    layout: "fit",
    al: true,
    initComponent: function () {
        var me = this;
        me.addFeatures([{
            action: 'parocess_Data',
            serveUrl: 'WFTask/Process',
            text: '处理',
            iconCls: 'icon_notebook',
            type: [1, 3]
        }, {
            action: 'search_Data',
            otype: 'nsearch',
            serveUrl: 'WFTask/Search',
            width: 300,
            type: [1]
        }]);
        //me.featureArray.push(me.addFeatures());
        this.callParent(arguments);
    },
    // 时间监听器
    listeners: {
        // 流程任务处理
        parocess_Data: function (but, record) {
            this.parocess_Data(but, record, this);
        },
    },
    // 流程任务处理
    parocess_Data: function (but, record, view) {
        //得到选中的数据
        var selected = view.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择处理数据");
            return;
        }
        if (!view.winEdit) {
            var me = this;
            //获得 添加窗体对象
            var object = {
                winTitle: but.text, winWidth: 1024, winTitle: '处理"' + selected.Title + '"', win: 'BPM.WFTask.Edit', config: {
                    saveUrl: but.menuObj.serveUrl,
                    onSaveSuccess: function () {
                        view.store.load();
                    }
                }
            }
            view.winEdit = util.createWindow(object);
        };
        //给添加窗体赋值
        var fHtml = view.winEdit.down("[name='FromHtml']");
        fHtml.html = '';
        util.request({
            url: 'WFForm/GetById',
            params: { Id: selected.FormId },
            async: true,
            success: function (result) {
                if (result.success) {
                    var html = Ext.create('Ext.Panel', {
                        html: result.data.Html
                    });

                    fHtml.add(html);
                }
            }
        });
        var form = view.winEdit.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(selected);
        // 打开窗体
        view.winEdit.show();
    }
});
