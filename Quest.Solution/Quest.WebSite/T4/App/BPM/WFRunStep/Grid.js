/**-----------------------------------------------------------------
* @explanation:流程步骤信息列表
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.WFRunStep.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_wfrunstep_grid',
    stateId: 'bpm_wfrunstep_grid',
    controllerName: 'WFRunStep',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'WFRunStep/GetAll',
    modelArray: ['SId', 'ParentId', 'Name', 'InstanceId', 'SenderId', 'FormId', 'SenderTime', 'FormUrl', 'ReceiveId', 'ReceiveTime', 'Valid', 'Status', 'Sort', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '步骤Id', dataIndex: 'SId', flex: 1 },
		{ text: '上一步骤Id', dataIndex: 'ParentId', flex: 1 },
		{ text: '步骤名称', dataIndex: 'Name', flex: 1 },
		{ text: '流程运行Id', dataIndex: 'InstanceId', flex: 1 },
		{ text: '发送人', dataIndex: 'SenderId', flex: 1 },
		{ text: '表单Id', dataIndex: 'FormId', flex: 1 },
		{ text: '发送时间', dataIndex: 'SenderTime', flex: 1 },
		{ text: 'FormUrl', dataIndex: 'FormUrl', flex: 1 },
		{ text: '接收人员Id', dataIndex: 'ReceiveId', flex: 1 },
		{ text: '接收时间', dataIndex: 'ReceiveTime', flex: 1 },
		{ text: '有效期', dataIndex: 'Valid', flex: 1 },
		{ text: '状态', dataIndex: 'Status', flex: 1 },
		{ text: '序号', dataIndex: 'Sort', flex: 1 },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1 },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1 },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1 },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1 },

    ],
    featureArray: [{
        action: 'add_Data',
        iconCls: 'icon_add',
        text: '新增',
        serveUrl: 'WFRunStep/Add',
        type: [1, 3]
    }, {
        action: 'updata_Data',
        iconCls: 'icon_edit',
        text: '修改',
        serveUrl: 'WFRunStep/Update',
        type: [1, 2]
    }, {
        action: 'del_Data',
        iconCls: 'icon_delete',
        text: '删除',
        serveUrl: 'WFRunStep/Delete',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        action: 'search_Data',
        serveUrl: 'WFRunStep/Search',
        width: 300,
        type: [1]
    }],
    // 事件监听器
    listeners: {
        // 添加
        add_Data: function (but, record) {
            this.add_Data(but, record, this);
        },
        // 修改
        updata_Data: function (but, record) {
            this.updata_Data(but, record, this);
        },
        // 删除
        del_Data: function (but, record) {
            this.del_Data(but, record, this);
        },
        // 查询
        search_Data: function (but, record) {
            this.search_Data(but, record, this);
        }
    },
    // 添加
    add_Data: function (but, record, grid) {
        var me = this;
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.WFRunStep.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function (action) {
                    if (action.result.success) {
                        grid.load_Data();
                    }
                }
            }
        }
        if(!grid.winEdit){
            grid.winEdit = util.createWindow(object);
        }
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        // 打开窗体
        grid.winEdit.show();
    },
    // 修改
    updata_Data: function (but, record, grid) {
        var me = this;
        //得到选中的数据
        var selected = grid.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择修改数据");
            return;
        }
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.WFRunStep.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function () {
                    grid.load_Data();
                }
            }
        }
        if(!grid.winEdit){
            grid.winEdit = util.createWindow(object);
        }
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(selected);
        // 打开窗体
        grid.winEdit.show();
    },
    // 删除
    del_Data: function (but, record, grid) {
        var me = this;
        //得到选中的数据
        var selected = grid.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择删除数据");
            return;
        }
        util.doDelete({
            url: but.menuObj.serveUrl,//提交地址
            params: { ids: selected.Id },//参数
            success: function (result) {
                if (result) {
                    grid.load_Data();
                }
            }
        });
    },
    // 加载数据
    load_Data: function () {
        this.store.load();
    },
    // 查询
    search_Data: function (but, record, grid) { }
});
