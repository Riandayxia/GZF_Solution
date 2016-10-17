/**-----------------------------------------------------------------
* @explanation:地址管理展示界面
* @created：XS
* @create time：2015/8/20
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Users.Address.List', {
    extend: 'app.user.NSimpleList',
    xtype: 'user_address_list',
    config: {
        isPage: false,
        moreBut: true,
        rootProperty: "data",
        addBut: true,
        cls: 'ux_list',
        search: false,//是否添加查询
        defTitle: '删除选中数据', // 标题
        ckId: 'Id',  //设置数据主键(可配置)
        listMenu: [
           { text: config.str.Insert, action: 'Insert' },
           { text: config.str.listDisplayAdjustment, action: 'SetColumn' },
           { text: config.str.listBatchEdit, action: 'AllSelect' },
        ],
        title: config.str.AddressList,
        dataUrl: config.url + '/Address/GetByUsersId',
        modelArray: ['Id', 'UsersId', 'Receiver', 'Mobile', 'TheCell', 'IsDefault', 'DetailedAddress', 'IsDeleted',
            'CreatedTime', 'LastUpdatedTime'],
        itemTpl: Ext.create('Ext.XTemplate',
           '<div class="container">',
               '<div class="header"><h1>{Receiver}<h1></div>',
               '<div class="header2">',
                  '<div class="ctent" style="width:40%;"><li class="title">联系电话</li><li><span class="_ctent">{Mobile}</span></li></div>',
                  '<div class="ctent" style="width:60%;"><li class="title">收货地址</li><li><span class="_ctent">{DetailedAddress}</span></li></div>',
               '</div>',
               '<div class="footer"style="height: 50px">',
                    //默认收货地址
                   '<div class="ctent" style="width:50%;">',
                      '<div style="float:left;width:40%;margin:15px 0 0 0"><span class="_ctent">{[this.IsDefault(values.IsDefault)]}</span></div>',
                      '<div style="float:left;width:50%;text-align:left;margin:22px 0 0 0"><span style="font-size: 150%; margin: 0 0 0 -40px;">设置为默认地址</span></div>',
                   '</div>',
                   //修改
                   '<div class="ctent" style="width:20%;">',
                    '<div style="float:center;width:120%;height: 70%;margin:13px 0 0 0"><img class="updates"  src="resources/images/Users/Address/laji@2x.png"/></div>',
                   '</div>',
                   //删除
                   '<div class="ctent" style="width:20%;">',
                      '<div style="float:center;width:120%;height: 70%;margin:13px 0 0 0"><img class="delete"  src="resources/images/Users/Address/laji@2x.png"/></div>',
                   '</div>',
              '</div>',
          '</div>', {
              IsDefault: function (val) {
                  switch (val) {
                      case false:
                          var str = '<img class="rad" src="resources/images/Users/Address/weigouxuan@2x.png" width="80px" height="80px"> ';
                          break;
                      case true:
                          var str = '<img class="rad" src="resources/images/Users/Address/yigouxuan@2x.png" width="80%" height="80%"> ';
                          break;
                  }
                  return str;
              }
          }),
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back");
            },
            //菜单加载前
            triggerago: function () {
                this._autoMenu = false;
            },
            //添加
            Insert: function (but, list) {
                this.getStructureMenu().hide();
                util.redirectTo("QST.Users.Address.Edit", "", {
                    parentUrl: "QST.Users.Address.List",
                    url: config.url + '/Address/Add',
                    data: { UsersId: this.UserId },
                });
            },
            //单击查看详细信息
            itemsingletap: function (list, index, target, record, e, eOpts) {
                //改变默认地址
                if (e.target.className == "rad") {
                    var record = util.copyObjects(record.data);
                    if (record.IsDefault == false) {
                        record.IsDefault = true
                        Ext.Ajax.request({
                            url: config.url + '/Address/Update',//提交地址
                            params: record,
                            success: function (response) {
                                var result = Ext.decode(response.responseText);
                                if (result.success) {
                                    util.showMessage(result.msg, true);
                                    list.rendering();
                                } else {
                                    util.showMessage(result.msg, true);
                                }
                            }
                        });
                    }
                }
                //编辑地址信息
                if (e.target.className == "updates") {
                    var record = util.copyObjects(record.data);
                    util.redirectTo("QST.Users.Address.Edit", "", {
                        parentUrl: "QST.Users.Address.List", data: record,
                        url: config.url + '/Address/Update'
                    });
                }
                //删除地址
                if (e.target.className == "delete") {
                    var record = util.copyObjects(record.data);
                    util.DoDelete({
                        url: config.url + '/Address/Delete',
                        params: { ids: record.Id },
                        success: function (response) {
                            list.rendering();
                        }
                    });
                }
            },
            // 列表展示设置
            SetColumn: function (but, view, record) {
                this.getStructureMenu().hide();
                util.redirectTo("SH.App.Systems.ListConfig.ViewEdit", "", { parentUrl: "SH.App.HRManagement.Askfoleave.Work.List", fieldArray: this.config.fieldArray, viewId: this.id });
            },
            //开始多选
            AllSelect: function (but, view) {
                this.beginSimple();
                this.getStructureMenu().hide();
            },
            // 多选回调函数
            SimpleSuccess: function (list, items, ids, simpleType) {
                //调用删除方法
                util.DoDelete({
                    url: config.url + '/Address/Delete',
                    params: { ids: ids.toString() },
                    success: function (response) {
                        list.rendering();
                    }
                });
            },
            // 更多功能
            More: function (but, view) {
                this.setStructureMenu(this.config.listMenu);
            }
        }
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params) {
            if (params.parentUrl) {
                this.backUrl = params.parentUrl;
            }
            if (params.data) {
                this.data = params.data
            }
            this.UserId = this.data.userId;
        }
        //设置查询参数
        this.getStore().setParams({ userId: this.UserId });
        //加载数据
        this.getStore().load();
    },
    //子界面返回到此界面时加载
    overViewResult: function (params) {
        //当编辑数据成功时加载数据
        this.getStore().setParams({ userId: this.UserId });
        this.getStore().load();
    }
})