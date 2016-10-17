/**-----------------------------------------------------------------
* @explanation活动咨询信息列表
* @created：HYF
* @create time：2016-10-12 09:35
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('Property.Community.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'property_community_grid',
    stateId: 'property_community_grid',
    controllerName: 'Community',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'Community/GetAll',
    modelArray: ['Title', 'ActivityTime', 'Publisher', 'Content', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '标题', dataIndex: 'Title', flex: 1 },
		{ text: '发布时间', dataIndex: 'ActivityTime', flex: 1 },
		{ text: '发布人', dataIndex: 'Publisher', flex: 1 },
		{ text: '活动内容', dataIndex: 'Content', flex: 1 },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1 },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1 },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1 },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1 },

    ],
    featureArray: [{
        winUrl: '',
        serveUrl: 'Community/OAdd',
        text: '新增',
        iconCls: 'icon_add',
        action: 'add_Data',
        type: [1, 3]
    }, {
        text: '修改',
        serveUrl: 'Community/Update',
        iconCls: 'icon_edit',
        action: 'updata_Data',
        type: [1, 2]
    }, {
        text: '删除',
        serveUrl: 'Community/Delete',
        iconCls: 'icon_delete',
        action: 'del_Data',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        serveUrl: 'Community/Search',
        width: 300,
        action: 'search_Data',
        type: [1]
    }],
    // 时间监听器
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
            winTitle: but.text, winWidth: 660, win: 'Property.Community.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function (action) {
                    if (action.result.success) {
                        grid.store.load();
                    }
                }
            }
        }
        grid.winEdit = util.createWindow(object);
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
            winTitle: but.text, winWidth: 800, win: 'Property.Community.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function () {
                    grid.store.load();
                }
            }
        }
        grid.winEdit = util.createWindow(object);
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        selected.ActivityTime = Ext.util.Format.date(selected.ActivityTime, "Y/m/d");
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
                    grid.store.load();
                }
            }
        });
    },
    // 查询
    search_Data: function (but, record, grid) { }
});
