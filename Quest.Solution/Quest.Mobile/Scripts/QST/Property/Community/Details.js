/**-----------------------------------------------------------------
* @explanation:首页内容详细信息
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Community.Details', {
    extend: 'app.user.NTemplate',
    xtype: 'property_Community_details',
    config: {
        moreBut: false,
        title: '社区详细',
        tpl: new Ext.XTemplate(
        '<div  class="details">{Content}',
        '</div>'
        ),
        listeners: {
            //返回
            Back: function () {
                util.redirectTo(this.backUrl, "back", {});
            },
    
            // 发表评论
            SubComment: function (content, but, view) {
                but.config.subComment(view.dataId, content);
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        //
        this.backUrl = params.parentUrl;
        if (params.data) {
            this.dataId = params.data.Id;
            this.setTplData(params.data);
            this._homeHeaderBar.setTitle(params.data.Title)
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);

        //me.add(SHUtil.SetBottomBar(me));
    },

})