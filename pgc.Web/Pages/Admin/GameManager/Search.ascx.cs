using kFrameWork.UI;
using pgc.Model.Patterns;
using pgc.Model.Enums;

public partial class Pages_Admin_GameManager_Search : BaseSearchControl<GameManagerPattern>
{
    public override GameManagerPattern Pattern
    {
        get
        {
            return new GameManagerPattern()
            {
                AccessLevel_ID = lkcAccessLevel.GetSelectedValue<long>(),
                ActivityStatus = lkcActivityStatus.GetSelectedValue<UserActivityStatus>(),
                Address = txtAddress.Text,
                //City_ID = lkcCity.GetSelectedValue<long>(),
                Email = txtEmail.Text,
                Mobile = txtMobile.Text,
                Name = txtName.Text,
                //NationalCode = txtNationalCode.Text,
                //Province_ID = lkcProvince.GetSelectedValue<long>(),
                
                Tel = txtTel.Text,
               
                SignUpPersianDate = pdrSignUpPersianDate.DateRange,
                Gender = lkcGender.GetSelectedValue<Gender>(),
                
            };
        }
        set
        {
            lkcAccessLevel.SetSelectedValue(value.AccessLevel_ID);
            lkcActivityStatus.SetSelectedValue(value.ActivityStatus);
            txtAddress.Text = value.Address;
            //lkcCity.SetSelectedValue(value.City_ID);
            txtEmail.Text = value.Email;
            
            txtMobile.Text = value.Mobile;
            txtName.Text = value.Name;
            //txtNationalCode.Text = value.NationalCode;
            
            //lkcProvince.SetSelectedValue(value.Province_ID);
           
            txtTel.Text = value.Tel;
            
            pdrSignUpPersianDate.DateRange = value.SignUpPersianDate;
            lkcGender.SetSelectedValue(value.Gender);
            
        }
    }
}