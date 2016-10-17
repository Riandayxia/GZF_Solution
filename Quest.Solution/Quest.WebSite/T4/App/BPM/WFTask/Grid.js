/**-----------------------------------------------------------------
* @explanation:任务信息信息列表
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.WFTask.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_wftask_grid',
    stateId: 'bpm_wftask_grid',
    controllerName: 'WFTask',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'WFTask/GetAll',
    modelArray: ['ParentId', 'InstanceId', 'MainId', 'StepId', 'FormId', 'StepName', 'Type', 'Title', 'SenderId', 'ReceiveId', 'FormUrl', 'OpenTime', 'CompletedTime', 'ActualFinishTime', 'Agreest', 'Comment', 'IsSign', 'Status', 'Note', 'Sort', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '上一任务Id', dataIndex: 'ParentId', flex: 1 },
		{ text: '实体Id', dataIndex: 'InstanceId', flex: 1 },
		{ text: '主体id', dataIndex: 'MainId', flex: 1 },
		{ text: '步骤Id', dataIndex: 'StepId', flex: 1 },
		{ text: '表单Id', dataIndex: 'FormId', flex: 1 },
		{ text: '步骤名称', dataIndex: 'StepName', flex: 1 },
		{ text: '任务类型', dataIndex: 'Type', flex: 1 },
		{ text: '标题', dataIndex: 'Title', flex: 1 },
		{ text: '发送人', dataIndex: 'SenderId', flex: 1 },
		{ text: '任务执行人', dataIndex: 'ReceiveId', flex: 1 },
		{ text: '表单地址', dataIndex: 'FormUrl', flex: 1 },
		{ text: '打开时间', dataIndex: 'OpenTime', flex: 1 },
		{ text: '规定完成时间', dataIndex: 'CompletedTime', flex: 1 },
		{ text: '实际完成时间', dataIndex: 'ActualFinishTime', flex: 1 },
		{ text: '是否同意', dataIndex: 'Agreest', flex: 1 },
		{ text: '意见', dataIndex: 'Comment', flex: 1 },
		{ text: '是否签章', dataIndex: 'IsSign', flex: 1 },
		{ text: '状态', dataIndex: 'Status', flex: 1 },
		{ text: '其它说明', dataIndex: 'Note', flex: 1 },
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
        serveUrl: 'WFTask/Add',
        type: [1, 3]
    }, {
        action: 'updata_Data',
        iconCls: 'icon_edit',
        text: '修改',
        serveUrl: 'WFTask/Update',
        type: [1, 2]
    }, {
        action: 'del_Data',
        iconCls: 'icon_delete',
        text: '删除',
        serveUrl: 'WFTask/Delete',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        action: 'search_Data',
        serveUrl: 'WFTask/Search',
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
            winTitle: but.text, winWidth: 660, win: 'BPM.WFTask.Edit', hideFields: me.typeFields, config: {
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
            winTitle: but.text, winWidth: 660, win: 'BPM.WFTask.Edit', hideFields: me.typeFields, config: {
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
