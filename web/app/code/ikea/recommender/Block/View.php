<?php
namespace ikea\recommender\Block;

class View extends \Magento\Framework\View\Element\Template
{

    protected $_session;

    public function __construct(\Magento\Framework\View\Element\Template\Context $context,
                                \Magento\Customer\Model\Session $session)
    {
        parent::__construct($context);
        $this->_session = $session;
    }


    public function printJavaOutput()
    {

        if ($this->_session->isLoggedIn()){

            return __(file_get_contents('http://java:4567/'.$this->_session->getCustomerId()));
        }
        return __("Nie jesteś zalogowany");

    }

}
