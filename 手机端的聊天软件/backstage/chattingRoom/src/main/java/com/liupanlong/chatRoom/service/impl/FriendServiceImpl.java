package com.liupanlong.chatRoom.service.impl;

import com.liupanlong.chatRoom.mapper.TbFriendMapper;
import com.liupanlong.chatRoom.mapper.TbFriendReqMapper;
import com.liupanlong.chatRoom.mapper.TbUserMapper;
import com.liupanlong.chatRoom.pojo.*;
import com.liupanlong.chatRoom.pojo.vo.FriendReq;
import com.liupanlong.chatRoom.pojo.vo.User;
import com.liupanlong.chatRoom.service.FriendService;
import com.liupanlong.chatRoom.util.IdWorker;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

@Service
@Transactional
public class FriendServiceImpl implements FriendService {

    @Autowired
    private IdWorker idWorker;

    @Autowired
    private TbUserMapper userMapper;

    @Autowired
    private TbFriendMapper tbFriendMapper;

    @Autowired
    private TbFriendReqMapper tbFriendReqMapper;

    @Override
    public void sendRequest(String fromUserid, String toUserid) {
        // 检查是否允许添加好友
        TbUser friend = userMapper.selectByPrimaryKey(toUserid);
        checkAllowToAddFriend(fromUserid,friend);

        // 添加好友请求
        TbFriendReq friendReq = new TbFriendReq();
        friendReq.setId(idWorker.nextId());
        friendReq.setFromUserid(fromUserid);
        friendReq.setToUserid(toUserid);
        friendReq.setCreatetime(new Date());
        friendReq.setStatus(0);
        tbFriendReqMapper.insert(friendReq);
    }

    @Override
    public List<FriendReq> findFriendReqByUserid(String userid) {
        // 根据用户id查询对应的好友请求
        TbFriendReqExample example = new TbFriendReqExample();
        TbFriendReqExample.Criteria criteria = example.createCriteria();

        criteria.andToUseridEqualTo(userid);
        criteria.andStatusEqualTo(0);

        List<TbFriendReq> friendReqList = tbFriendReqMapper.selectByExample(example);
        List<FriendReq> friendUserList = new ArrayList<>();
        // 根据好友请求，将发起好友请求的用户信息返回
        for (TbFriendReq friendReq: friendReqList) {
            TbUser tbUser = userMapper.selectByPrimaryKey(friendReq.getFromUserid());
            FriendReq user = new FriendReq();
            BeanUtils.copyProperties(tbUser,user);
            user.setId(friendReq.getId());
            friendUserList.add(user);
        }
        return friendUserList;
    }

    @Override
    public void acceptFriendReq(String reqid) {
        // 将Status设置为1
        TbFriendReq friendReq = tbFriendReqMapper.selectByPrimaryKey(reqid);
        friendReq.setStatus(1);
        tbFriendReqMapper.updateByPrimaryKey(friendReq);

        // 如果存在，将另一条请求也置为1
        TbFriendReqExample example = new TbFriendReqExample();
        TbFriendReqExample.Criteria criteria = example.createCriteria();

        criteria.andToUseridEqualTo(friendReq.getFromUserid());
        criteria.andStatusEqualTo(0);
        List<TbFriendReq> friendReqList= tbFriendReqMapper.selectByExample(example);

        if(friendReqList != null && friendReqList.size() > 0){
            TbFriendReq newReq = friendReqList.get(0);
            newReq.setStatus(1);
            tbFriendReqMapper.updateByPrimaryKey(newReq);
        }

        TbFriendExample friendExample = new TbFriendExample();
        TbFriendExample.Criteria friendCriteria = friendExample.createCriteria();

        friendCriteria.andUseridEqualTo(friendReq.getToUserid());
        friendCriteria.andFriendsIdEqualTo(friendReq.getFromUserid());
        List<TbFriend> friendList = tbFriendMapper.selectByExample(friendExample);

        //System.out.println("数据库中的朋友信息："+friendList);
        if(friendList == null || friendList.size() <= 0){
            // 将互相添加好友
            TbFriend friend1 = new TbFriend();
            friend1.setId(idWorker.nextId());
            friend1.setUserid(friendReq.getFromUserid());
            friend1.setFriendsId(friendReq.getToUserid());
            friend1.setCreatetime(new Date());

            TbFriend friend2 = new TbFriend();
            friend2.setId(idWorker.nextId());
            friend2.setFriendsId(friendReq.getFromUserid());
            friend2.setUserid(friendReq.getToUserid());
            friend2.setCreatetime(new Date());

            tbFriendMapper.insert(friend1);
            tbFriendMapper.insert(friend2);
        }
    }

    @Override
    public void ignoreFriendReq(String reqid) {
        TbFriendReq friendReq = tbFriendReqMapper.selectByPrimaryKey(reqid);
        tbFriendReqMapper.deleteByPrimaryKey(reqid);
    }

    @Override
    public List<User> findFriendByUserid(String userid) {
        TbFriendExample frindExample = new TbFriendExample();
        TbFriendExample.Criteria criteria = frindExample.createCriteria();

        criteria.andUseridEqualTo(userid);

        List<TbFriend> tbFriendList = tbFriendMapper.selectByExample(frindExample);

        List<User> friendList = new ArrayList<>();

        for (TbFriend friend : tbFriendList) {
            TbUser user = userMapper.selectByPrimaryKey(friend.getFriendsId());
            User myFriend = new User();
            BeanUtils.copyProperties(user,myFriend);
            friendList.add(myFriend);
        }
        return friendList;
    }

    /**
     * 检查是否允许添加好友
     * @param userid
     * @param friend
     */
    private void checkAllowToAddFriend(String userid, TbUser friend){
        //用户不能添加自己为好友
        if(friend.getId().equals(userid)){
            throw new RuntimeException("不能添加自己为好友");
        }

        // 用户不能重复添加好友
        // 如果已经是好友了，就不能再次添加
        TbFriendExample friendExample = new TbFriendExample();
        TbFriendExample.Criteria friendCriteria = friendExample.createCriteria();

        friendCriteria.andUseridEqualTo(userid);
        friendCriteria.andFriendsIdEqualTo(friend.getId());

        List<TbFriend> friendList = tbFriendMapper.selectByExample(friendExample);
        if(friendList != null && friendList.size() > 0){
            throw new RuntimeException(friend.getUsername() + "已经是你的好友了");
        }

        // 判断是否有好友请求存在，如果存在就不能发起请求
        TbFriendReqExample friendReqExample = new TbFriendReqExample();
        TbFriendReqExample.Criteria friendReqCriteria = friendReqExample.createCriteria();

        // 当前用户发送的好友请求
        friendReqCriteria.andFromUseridEqualTo(userid);
        friendReqCriteria.andToUseridEqualTo(friend.getId());
        // 而且这个请求还没有被处理
        friendReqCriteria.andStatusEqualTo(0);

        List<TbFriendReq> friendReqList = tbFriendReqMapper.selectByExample(friendReqExample);

        if(friendReqList != null && friendReqList.size() > 0){
            throw new RuntimeException("已经申请过了");
        }
    }
}
