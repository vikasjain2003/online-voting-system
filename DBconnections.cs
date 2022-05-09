package com;
import java.sql.*;
public class DBconnection
{	
	Connection con;
	ResultSet rs=null;
	public DBconnection()
	{
		try
		{
			Class.forName("com.mysql.jdbc.Driver");
	con=DriverManager.getConnection("jdbc:mysql://localhost/kr","root","");
		}
		catch(Exception e)
		{
			System.out.println(e);
		}
	}

	public ResultSet loginValidate(String name,String password)
	{
		try
		{
			PreparedStatement ps=con.prepareStatement("select * from login where username=? and password =? and status=?");
			ps.setString(1,name);
			ps.setString(2,password);
			ps.setString(3,"accepted");
			rs=ps.executeQuery();
		}
		catch(Exception e)
		{
			System.out.println(e);
		}
		return rs;
	}
	
	public void addCandidate(String canusrname,String canpassword,String canname,String canaddress,String cangender,String candob,String constituency,String canphno,String canemailid,String canddescription,String party,String cansymbol)
	{
		int constituencyid=Integer.parseInt(constituency);		
		ResultSet rs = null;
		int id=0;
		try
		{
			PreparedStatement ps=con.prepareStatement("insert into login(username,password,role) values(?,?,?)");
			ps.setString(1,canusrname);
			ps.setString(2,canpassword);
			ps.setString(3,"candidate");
			ps.executeUpdate();
	
			PreparedStatement ps1=con.prepareStatement("select max(pkiloginid) as pkiloginid from login");
			rs=ps1.executeQuery();
			while(rs.next())
			{
				id=rs.getInt("pkiloginid");
			}
			
			PreparedStatement ps2=con.prepareStatement("insert into candidatereg(fkiloginid,candidatename,candidateaddress,cangender,candateofbirth,fkiconstituencyid,phone,emailid,candesc,status,party,cansymbol) values(?,?,?,?,?,?,?,?,?,?,?,?)");
			ps2.setInt(1,id);
			ps2.setString(2,canname);
			ps2.setString(3,canaddress);
			ps2.setString(4,cangender);
			ps2.setString(5,candob);
			ps2.setInt(6,constituencyid);
			ps2.setString(7,canphno);
			ps2.setString(8,canemailid);
			ps2.setString(9,canddescription);
			ps2.setString(10,"accepted");
			ps2.setString(11,party);
			ps2.setString(12,cansymbol);
			ps2.executeUpdate();

			PreparedStatement ps3=con.prepareStatement("select max(pkicandidateid) as pkicandidateid from candidatereg");
			rs=ps3.executeQuery();
			while(rs.next())
			{
				id=rs.getInt("pkicandidateid");
			}

			PreparedStatement ps4=con.prepareStatement("insert into result(fkicandidateid,count) values(?,?)");
			ps4.setInt(1,id);
			ps4.setInt(2,0);
			ps4.executeUpdate();

			PreparedStatement ps5=con.prepareStatement("update constituency set candidatenumber=candidatenumber+1 where pksiconstituencyid=?");
			ps5.setInt(1,constituencyid);
			ps5.executeUpdate();
			
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
        }

	public void addVoter(String voterusrname,String voterpassword,String votername,String voteraddresss,String dateofbirth,String location,String constituency,String emailid,String phno,String voterid,String gender,String age,String voterimg)
	{
		int locationid=Integer.parseInt(location);
		int constituencyid=Integer.parseInt(constituency);
		int voterid1=Integer.parseInt(voterid);
		int age1=Integer.parseInt(age);
		ResultSet rs = null;
		int id=0;
		try
		{
			PreparedStatement ps=con.prepareStatement("insert into login(username,password,role) values(?,?,?)");
			ps.setString(1,voterusrname);
			ps.setString(2,voterpassword);
			ps.setString(3,"voter");
			ps.executeUpdate();
	
			PreparedStatement ps1=con.prepareStatement("select max(pkiloginid) as pkiloginid from login");
			rs=ps1.executeQuery();
			while(rs.next())
			{
				id=rs.getInt("pkiloginid");
			}
			
			PreparedStatement ps2=con.prepareStatement("insert into voterreg(fkiloginid,votername,voteraddress,votergender,voterofbirth,fkiconstituencyid,fkilocationid,phone,emailid,voterid,status,age,voterimg) values(?,?,?,?,?,?,?,?,?,?,?,?,?)");
			ps2.setInt(1,id);
			ps2.setString(2,votername);
			ps2.setString(3,voteraddresss);
			ps2.setString(4,gender);
			ps2.setString(5,dateofbirth);
			ps2.setInt(6,constituencyid);
			ps2.setInt(7,locationid);
			ps2.setString(8,phno);
			ps2.setString(9,emailid);
			ps2.setInt(10,voterid1);
			ps2.setString(11,"accepted");
			ps2.setInt(12,age1);
			ps2.setString(13,voterimg);
			ps2.executeUpdate();

			PreparedStatement ps3=con.prepareStatement("update location set voterno=voterno+1 where pkilocationid=?");
			ps3.setInt(1,locationid);
			ps3.executeUpdate();

			PreparedStatement ps4=con.prepareStatement("update constituency set voternumber=voternumber+1 where pksiconstituencyid=?");
			ps4.setInt(1,constituencyid);
			ps4.executeUpdate();
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
        }
	
	public void addLocation(String locationname)
	{
		try
		{
			PreparedStatement ps=con.prepareStatement("insert into location(locationname) values(?)");
			ps.setString(1,locationname);
			ps.executeUpdate();
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
        }

	public void addConstituency(String location,String constituencyname)
	{
		int locationid=Integer.parseInt(location);
		try
		{
			PreparedStatement ps=con.prepareStatement("insert into constituency(fkilocationid,constituencyname) values(?,?)");
			ps.setInt(1,locationid);
			ps.setString(2,constituencyname);
			ps.executeUpdate();
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
        }
	
	public ResultSet listVoters()
	{
		try
		{
			PreparedStatement ps=con.prepareStatement("select * from voterreg,constituency where pksiconstituencyid=fkiconstituencyid");
			rs=ps.executeQuery();
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
		return rs;
        }

	public void addElectionAction(String electionname,String constituency,String terminationdate)
	{
		System.out.println(electionname+constituency+terminationdate);
		int constituencyid=Integer.parseInt(constituency);
		try
		{
			PreparedStatement ps=con.prepareStatement("insert into electionmaster(electionname,fkiconstituencyid,terminationdate) values(?,?,?)");
			
			ps.setString(1,electionname);
			ps.setInt(2,constituencyid);
			ps.setString(3,terminationdate);
			ps.executeUpdate();

			PreparedStatement ps1=con.prepareStatement("update result set termdate=?");
			
			ps1.setString(1,terminationdate);
			ps1.executeUpdate();
		}
		catch(Exception e)
		{
			System.out.println(e);
		}	
        }
	

	public ResultSet listElectionMaster(int constituency)
	{
		try
		{
			PreparedStatement ps=con.prepareStatement("select * from electionmaster where fkiconstituencyid=? and terminationdate > '(select curdate())'");
			ps.setInt(1,constituency);
			rs=ps.executeQuery();
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
		return rs;	
        }
	
		public ResultSet getVotersCount()
	{
		try
		{
			PreparedStatement ps=con.prepareStatement("select count(*) from voterreg");
			rs=ps.executeQuery();
		}
		catch (Exception e)
		{
			System.out.println(e);
		}
		return rs;	
        }

	public void vote(String electionname,String candidate,int logid)
	{
		int candidateid=Integer.parseInt(candidate);
		int electionid=Integer.parseInt(electionname);
		try
		{
			PreparedStatement ps=con.prepareStatement("insert into electiondetails(fkivoterid,fkielectionmasterid) values(?,?)");
			ps.setInt(1,logid);
			ps.setInt(2,electionid);
			ps.executeUpdate();

			PreparedStatement ps1=con.prepareStatement("update result set count=count+1 where fkicandidateid=?");
			ps1.setInt(1,candidateid);
			ps1.executeUpdate();	
		}
		catch(Exception e)
		{
			System.out.println(e);
		}
	
        }	

}



























