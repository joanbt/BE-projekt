<?php
namespace ikea\recommender\Block;
class View extends \Magento\Framework\View\Element\Template
{

protected $_productRepository; 


    public function __construct(
	\Magento\Framework\View\Element\Template\Context $context,
 	\Magento\Catalog\Api\ProductRepositoryInterface $productRepository
)
    {
        parent::__construct($context);
    	$this->_productRepository = $productRepository;
    }


    public function printJavaOutput()
    {
	$resp=file_get_contents('http://java:4567/');
	$ret="";
	$size=3;
	for($i=0;$i<$size;$i++)
	{
		$start=strpos($resp,"[");
		$end=strpos($resp,"]");
		$sub[$i]= substr($resp, $start, $end-$start+1);
		$resp=substr($resp,$end+1);

	}
	for($i=0;$i<$size;$i++)
	{

		$start=strpos($sub[$i],":");
		$end=strpos($sub[$i],",");
		$item_id[$i]=substr($sub[$i], $start+1, $end-$start-1);
		$product = $this->_productRepository->getById($item_id[$i]);
 		$productImageUrl = $this->getUrl('pub/media/catalog').'product'.$product->getImage();


		$ret=$ret.$item_id[$i]."   <a>".$product->getProductUrl().$productImageUrl."</a>"."<br>";

	}
        return __($ret);
    }

}