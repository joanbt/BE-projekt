<?php
namespace ikea\recommender\Block;
class View extends \Magento\Framework\View\Element\Template
{
    public function __construct(\Magento\Framework\View\Element\Template\Context $context)
    {
        parent::__construct($context);
    }


    public function printJavaOutput()
    {
        return __(file_get_contents('http://java:4567/'));
    }

}
