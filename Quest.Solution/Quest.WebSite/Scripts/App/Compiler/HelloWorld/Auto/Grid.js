/*
数据表
*/
Ext.define('Compiler.HelloWorld.Auto.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'dbtable_auto_grid',
    stateId: 'dbtable_auto_grid',
    controllerName: 'HelloWorld',
    al: true,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'HelloWorld/GetAll',
    modelArray: ['Id', 'CreatedTime', 'LastUpdatedTime', 'Name', 'Desc', 'Creator'],
    columns: [
        { text: '唯一标识', dataIndex: 'Id', flex: 1, minWidth: 0, hidden: true },
        { text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, minWidth: 0, hidden: true },
        { text: '表名称', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '创建者', dataIndex: 'Creator', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreatedTime', flex: 1, minWidth: 0 },
        { text: '描述', dataIndex: 'Desc', flex: 1, minWidth: 0 }
    ],
    featureArray: [{
        winUrl: '',
        serveUrl: 'HelloWorld/Add',
        text: '新增',
        iconCls: 'icon_add',
        action: 'add_Data',
        type: [1, 3]
    }, {
        text: '修改',
        serveUrl: 'HelloWorld/Update',
        iconCls: 'icon_edit',
        action: 'updata_Data',
        type: [1, 2]

    }, {
        text: '删除',
        serveUrl: 'HelloWorld/Del',
        iconCls: 'icon_delete',
        action: 'del_Data',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        serveUrl: 'HelloWorld/Search',
        width: 300,
        action: 'search_Data',
        type: [1]
    }],
    //监听器
    listeners: {
        // 添加
        add_Data: function (but, record) {
            var me = this;
            //获得 添加窗体对象
            var object = {
                winTitle: but.text, winWidth: 660, win: 'Compiler.HelloWorld.Auto.Edit', hideFields: me.typeFields, config: {
                    saveUrl: but.menuObj.serveUrl,
                    onSaveSuccess: function (action) {
                        if (action.result.success) {
                            me.store.load();
                        }
                    }
                }
            }
            me.winEdit = util.createWindow(object);
            var form = me.winEdit.down("form").getForm();
            form.reset();
            // 打开窗体
            me.winEdit.show();
        },
        // 修改
        updata_Data: function (but, record) {
            alert('修改')
        },
        // 删除
        del_Data: function (but, record) {
            alert('删除')
        },
        // 查询
        search_Data: function (but, record) {
            alert('查询')
        }
    }
});

