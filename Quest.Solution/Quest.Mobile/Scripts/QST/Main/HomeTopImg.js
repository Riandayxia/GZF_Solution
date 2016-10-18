/**-----------------------------------------------------------------
* @explanation:主界面头部滚动图片
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Main.HomeTopImg', {
    extend: 'Ext.Carousel',//'Ext.Container',
    xtype: 'home_top_img',
    fullscreen: true,
    defaults: {
        styleHtmlContent: true
    },
    config: {
        ui: 'dark',
        height: 160,
        items: [
            {
                xtype: 'panel',
                html: '<img src="resources/images/main/banner@2x.png" class="topImg" fire="onDelete">'
            },
            {
                xtype: 'panel',
                html: '<img src="resources/images/main/ganhong@2x.png" class="topImg" fire="onDelete">'
            },
            {
                xtype: 'panel',
                html: '<img src="resources/images/main/xianguobanner@2x.png"  class="topImg" fire="onDelete">'
            }
        ]
    }
});
