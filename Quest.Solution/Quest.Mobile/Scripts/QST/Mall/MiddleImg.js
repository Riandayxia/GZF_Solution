/**-----------------------------------------------------------------
* @explanation:中间滚动图片
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Mall.MiddleImg', {
    extend: 'Ext.Carousel',
    xtype: 'mall_middle_img',
    defaults: {
        styleHtmlContent: true
    },
    config: {
        ui: 'dark',
        height: 120,
        items: [
            {
                xtype: 'panel',
                html: '<img src="resources/images/Mall-1.png" class="topImg" fire="onDelete">'
            },
            {
                xtype: 'panel',
                html: '<img src="resources/images/5.jpg" class="topImg" fire="onDelete">'
            },
            {
                xtype: 'panel',
                html: '<img src="resources/images/6.jpg"  class="topImg" fire="onDelete">'
            }
        ]
    }
});
