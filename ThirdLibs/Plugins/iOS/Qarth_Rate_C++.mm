#import "Qarth_Rate_C++.h"

extern "C"
{
    void Qarth_Rate_RequestReview()
    {
        if([SKStoreReviewController class])
        { 
            [SKStoreReviewController requestReview]; 
        }
    }

    bool Qarth_Rate_IsInSandbox()
    {
        NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
        NSString *receiptURLString = [receiptURL path];
        BOOL isSandboxReceipt =  ([receiptURLString rangeOfString:@"sandboxReceipt"].location != NSNotFound);
        return isSandboxReceipt;
    }
}
