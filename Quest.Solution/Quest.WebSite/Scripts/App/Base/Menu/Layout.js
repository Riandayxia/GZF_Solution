/**-----------------------------------------------------------------
* @explanation:菜单布局
* @created：rainday
* @create time：2016-08-19 09:34
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('Base.Menu.Layout', {
    extend: 'Ext.container.Container',
    xtype: 'base_menu_layout',
    layout: 'border',
    requires: [
        'Base.Menu.Tree',
        'Base.Menu.Grid'
    ],
    items: [
        {
            region: 'west',
            split: true,
            width: '15%',
            minWidth: 120,
            xtype: 'base_menu_tree',
            listeners: {
                itemclick: function (e, record) {
                    this.up('base_menu_layout').getByParentName(record.data.Tobject)
                }
            }
        }, {
            region: 'center',
            xtype: 'base_menu_grid',
        }
    ],
    // 初始化
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
        me.set_Listener();
    },
    // 加载子级菜单
    getByParentName: function (data) {
        var me = this;
        me.ObjMenu = data;
        me.down('base_menu_grid').store.load({ params: { pName: data.ControllerName } });
    },
    // 设置事件
    set_Listener: function () {
        var me = this;
        var grid = me.down('base_menu_grid');
        //数据列表 重写
        Ext.override(grid, {
            // 添加数据
            add_Data: function (but, record, view) {
                var menu = me.ObjMenu;
                if (menu && menu.ControllerName) {
                    //获得 添加窗体对象
                    var object = {
                        winTitle: but.text, winWidth: 600, win: 'Base.Menu.Edit', config: {
                            saveUrl: but.menuObj.serveUrl,
                            onSaveSuccess: function (action) {
                                if (action.result.success) {
                                    view.store.load({ params: { pName: menu.ControllerName } });
                                }
                            }
                        }
                    }
                    if (!view.winEdit) {
                        view.winEdit = util.createWindow(object);
                    }
                    var form = view.winEdit.down("form").getForm();
                    form.reset();
                    //给添加窗体赋值
                    form.setValues({ ParentName: menu.ControllerName });
                    // 打开窗体
                    view.winEdit.show();
                } else {
                    Ext.Msg.alert("提示", "请选择数据表数据！");
                    return;
                }
            },
            // 加载数据
            load_Data: function () {
                var menu = me.ObjMenu;
                grid.store.load({ params: { pName: menu.ControllerName } });
            }
        });
    }
});
