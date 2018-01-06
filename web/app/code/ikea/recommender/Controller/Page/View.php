<?php
namespace ikea\recommender\Controller\Page;
use Magento\Framework\App\Action\Context;
use \Magento\Framework\View\Result\PageFactory;

class View extends \Magento\Framework\App\Action\Action
{
    /**
     * @var \Magento\Framework\Controller\Result\JsonFactory
     */
    protected $_pageFactory;

    public function __construct(
        Context $context,
        PageFactory $pageFactory)
    {

        $this->_pageFactory = $pageFactory;
        parent::__construct($context);
    }
    /**
     * View  page action
     *
     * @return \Magento\Framework\Controller\ResultInterface
     */
    public function execute()
    {
        $resultPage = $this->_pageFactory->create();
        $resultPage->getConfig()->getTitle()->prepend(__('Polecane produkty'));
        return $resultPage;
    }
}
