<?php
namespace ikea\recommender\Block;
class View extends \Magento\Framework\View\Element\Template
{

protected $_productRepository; 


    public function __construct(
	\Magento\Framework\View\Element\Template\Context $context,
 	\Magento\Catalog\Api\ProductRepositoryInterface $productRepository,
	\Magento\Customer\Model\Session $customerSession
)
    {
        parent::__construct($context);
    	$this->_productRepository = $productRepository;
	$this->customerSession = $customerSession;
    }


    public function printJavaOutput()
    {
	$resp=file_get_contents('http://java:4567/'.$this->customerSession->getCustomer()->getId());
	$ret="";
	$start=strpos($resp,"{");
	$end=strpos($resp,"}");
	$client_id= substr($resp, $start, $end-$start+1);
	$resp=substr($resp,$end);


	$size=3;
	for($i=0;$i<$size;$i++)
	{
		$start=strpos($resp,"[");
		$end=strpos($resp,"]");
		$sub[$i]= substr($resp, $start, $end-$start+1);
		$resp=substr($resp,$end+1);

	}
	$ret="<table>";
	for($i=0;$i<$size;$i++)
	{

		$start=strpos($sub[$i],":");
		$end=strpos($sub[$i],",");
		$item_id[$i]=substr($sub[$i], $start+1, $end-$start-1);
		if($item_id[$i]==null)
			$ret=$ret."Brak danych<br>";
		else
		{
		$product = $this->_productRepository->getById($item_id[$i]);
 		$productImageUrl = $this->getUrl('pub/media/catalog').'product'.$product->getImage();
		$ret=$ret."<tr><td>".$product->getPrice().$this->_storeManager->getStore()->getCurrentCurrency()->getCode()." </td><td> ".$product->getName()." <br><a href=\"".$product->getProductUrl()."\">link</a></td><td> <img src=\"".$productImageUrl."\"></td></tr>";
		}
	}
        return __($ret);
    }

}
