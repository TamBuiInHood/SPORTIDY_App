using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Utils.Mail
{
    public class SendOTPTemplate
    {

        public static string EmailSendOTPResetPassword(string email, string otpCode)
        {
            #region style

            string style =

            @"<style>
                body {
                  font-family: Arial, sans-serif;
                  margin: 0;
                  padding: 0;
                }
                .container {
                  width: 550px;
                  margin: 0 auto;
                  padding: 20px;
                }
                .header {
                  display: flex;
                  align-items: center;
                  background-color: #f7f7f7;
                  padding: 10px;
                }
                .header img {
                  object-fit: cover;
                  padding-top: 10px;
                  width: 100px;
                }
                .otp {
                  border: #0098af solid 2px;
                  padding: 8px;
                  text-align: center;
                  margin: auto 35%;
                }
                .otp span {
                  color: #1cbcd4;
                  font-weight: bold;
                  font-size: 22px;
                  letter-spacing: 3px;
                }
                .footer {
                  background-color: #f7f7f7;
                  padding: 10px;
                }
                .footer img {
                  width: 100px;
                }
                .info p {
                  font-size: 13px;
                }
                .info p:last-child {
                  margin-bottom: 0;
                }
            </style>";
            #endregion style

            #region body

            string body =
                $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Confirm Email</title>
                    {style}
                </head>
                <body>
                    <div class=""container"">
                      <div class=""header"">
                        <img
                          src=""https://firebasestorage.googleapis.com/v0/b/sportidy-447fd.appspot.com/o/SPORTYDI.png?alt=media&token=e5dbca1b-32c2-463b-8c2d-c1c914dccf17""
                          alt
                        />
                        <div style=""border-left: 2px solid #1cbcd4; margin-left: 20px"">
                          <h2 style=""margin-left: 125px; margin-top: 37px"">Đặt lại mật khẩu</h4>
                        </div>
                      </div>
                      <div class=""content"">
                        <div>
                          <h3>Đặt lại mật khẩu Sportidy</h3>
                          <p style=""font-size: 15px"">Bạn vừa thực hiện yêu cầu đặt lại mật khẩu cho tài khoản {email} tại Sportidy.</p>
                          <p style=""font-size: 15px"">Đây là mã xác thực OTP của bạn</p>
                        </div>
                        <div class=""otp"">
                          <span>{otpCode}</span>
                        </div>
                        <div class=""note"">
                          <p style=""font-size: 15px; font-style: italic; color: red"">
                            * Lưu ý: Không chia sẻ cho bất kì bên liên quan nào.
                          </p>
                        </div>
                      </div>
                      <div class=""footer"">
                        <img
                          src=""https://firebasestorage.googleapis.com/v0/b/sportidy-447fd.appspot.com/o/SPORTYDI.png?alt=media&token=e5dbca1b-32c2-463b-8c2d-c1c914dccf17""
                          alt=""logo""
                          width=""80px""
                        />
                        <div class=""info"">
                          <p>Điện thoại: +84 988 889 898</p>
                          <p>Email: sportidy839@gmail.com</p>
                          <p>Địa chỉ: Đại học FPT thành phố Hồ Chí Minh</p>
                        </div>
                      </div>
                    </div>
                </body>
                </html>
                ";
            #endregion body

            return body;
        }
    }
}
